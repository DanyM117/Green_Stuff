using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Green_Stuff.Models;
using System.Linq;
using Green_Stuff.Models.ViewModels;

namespace Green_Stuff.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DbLabpwebContext _context;

        public ProductsController(DbLabpwebContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString, int? categoryId, int? sizeId, int? waterDemandId, int? sunExposureId, decimal? minPrice, decimal? maxPrice)
        {
            var products = _context.Warehouses
                .Include(p => p.oCategories)
                .Include(p => p.oSizes)
                .Include(p => p.oWaterDemmand)
                .Include(p => p.oExpositionToSun)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }
            if (categoryId.HasValue)
            {
                products = products.Where(p => p.Idcategory == categoryId.Value);
            }
            if (sizeId.HasValue)
            {
                products = products.Where(p => p.Idsize == sizeId.Value);
            }
            if (waterDemandId.HasValue)
            {
                products = products.Where(p => p.Idwater == waterDemandId.Value);
            }
            if (sunExposureId.HasValue)
            {
                products = products.Where(p => p.Idsun == sunExposureId.Value);
            }
            if (minPrice.HasValue)
            {
                products = products.Where(p => p.UnitPrice >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.UnitPrice <= maxPrice.Value);
            }

            ViewData["Categories"] = _context.Categories.ToList();
            ViewData["Sizes"] = _context.Sizes.ToList();
            ViewData["WaterDemands"] = _context.WaterDemmands.ToList();
            ViewData["SunExposures"] = _context.ExpositionToSuns.ToList();

            return View(products.ToList());
        }
        public IActionResult Details(int id)
        {
            var product = _context.Warehouses
                .Include(p => p.oCategories)
                .Include(p => p.oSizes)
                .Include(p => p.oWaterDemmand)
                .Include(p => p.oExpositionToSun)
                .FirstOrDefault(p => p.IdItem == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            List<CartItem> cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();

            var product = _context.Warehouses.Find(productId);

            if (product != null)
            {
                var cartItem = cart.FirstOrDefault(c => c.Product.IdItem == productId);

                if (cartItem != null)
                {
                    cartItem.Quantity += quantity;
                }
                else
                {
                    cart.Add(new CartItem { Product = product, Quantity = quantity });
                }

                HttpContext.Session.SetObject("Cart", cart);
            }

            return RedirectToAction("Index", "Cart");
        }

    }
}
