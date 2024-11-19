using Microsoft.AspNetCore.Mvc;
using Green_Stuff.Models.ViewModels;
using Green_Stuff.Models;
using Microsoft.EntityFrameworkCore;

namespace Green_Stuff.Controllers
{
    public class CartController : Controller
    {
        private readonly DbLabpwebContext _context;

        public CartController(DbLabpwebContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
        }

        [HttpPost]
        public IActionResult Remove(int productId)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();
            cart.RemoveAll(c => c.Product.IdItem == productId);
            HttpContext.Session.SetObject("Cart", cart);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var userId = HttpContext.Session.GetInt32("UserID");

            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Acceso");
            }

            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();

            var paymentCards = await _context.PaymentCards
                .Where(c => c.Iduser == userId.Value)
                .ToListAsync();

            var checkoutViewModel = new CheckoutViewModel
            {
                CartItems = cart,
                PaymentCards = paymentCards
            };

            return View(checkoutViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckoutConfirm(int selectedCardId)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();
            var userId = HttpContext.Session.GetInt32("UserID");

            if (cart.Count == 0 || !userId.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var sale = new Sale
                    {
                        Iduser = userId.Value,
                        Idcard = selectedCardId,
                        CreationDate = DateTime.Now,
                        TotalAmmount = cart.Sum(c => c.Product.UnitPrice * c.Quantity),
                        TotalQuantity = cart.Sum(c => c.Quantity)
                    };

                    _context.Sales.Add(sale);
                    await _context.SaveChangesAsync();

                    foreach (var item in cart)
                    {
                        var warehouseItem = await _context.Warehouses.FindAsync(item.Product.IdItem);
                        if (warehouseItem == null || warehouseItem.Quantity < item.Quantity)
                        {
                            throw new Exception($"No hay suficiente stock para el producto {item.Product.Name}");
                        }
                        var saleDetail = new SaleDetail
                        {
                            Idsale = sale.IdSale,
                            Iditem = item.Product.IdItem,
                            UnitPrice = item.Product.UnitPrice,
                            Quantity = item.Quantity,
                            Subtotal = item.Product.UnitPrice * item.Quantity,
                            ModificationDate = DateTime.Now
                        };

                        _context.SaleDetails.Add(saleDetail);

                        warehouseItem.Quantity -= item.Quantity;
                        _context.Warehouses.Update(warehouseItem);
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    HttpContext.Session.Remove("Cart");

                    return RedirectToAction("PurchaseSuccess");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", "Ocurrió un error al procesar la compra: " + ex.Message);
                    var paymentCards = await _context.PaymentCards
                        .Where(c => c.Iduser == userId.Value)
                        .ToListAsync();

                    var checkoutViewModel = new CheckoutViewModel
                    {
                        CartItems = cart,
                        PaymentCards = paymentCards
                    };

                    return View("Checkout", checkoutViewModel);
                }
            }
        }


        public IActionResult PurchaseSuccess()
        {
            return View();
        }
    }
}
