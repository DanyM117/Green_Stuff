using Green_Stuff.Models;
using Green_Stuff.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Green_Stuff.Controllers
{
    public class PaymentCardsController : Controller
    {
        private readonly DbLabpwebContext _DBContext;
        public PaymentCardsController(DbLabpwebContext context)
        {
            _DBContext = context;
        }
        // GET: PaymentCardsController
        public ActionResult Index()
        {
            List<PaymentCard> listCards = _DBContext.PaymentCards.Include(o => o.oUsers).ToList();
            return View(listCards);
        }
        [HttpGet]
        // GET: PaymentCardsController/Details/5
        public ActionResult Detail(int IdCard)
        {
            PaymentCardsVM oCardVM = new PaymentCardsVM ()
            {
                oPaymentCards = new PaymentCard(),
                oPaymentUsersList = _DBContext.Users.Select(users => new SelectListItem()
                {
                    Text = users.Username,
                    Value = users.IdUser.ToString()
                }).ToList()
            };
            if (IdCard != 0)
            {
                oCardVM.oPaymentCards = _DBContext.PaymentCards.Find(IdCard);
            }
            return View(oCardVM);
        }
        [HttpPost]
        public ActionResult Detail(PaymentCardsVM oCardVM)
        {
            if (oCardVM.oPaymentCards.IdCard == 0)
            {
                _DBContext.PaymentCards.Add(oCardVM.oPaymentCards);
            }
            else
            {
                _DBContext.PaymentCards.Update(oCardVM.oPaymentCards);
            }
            _DBContext.SaveChanges();
            return RedirectToAction("Index", "PaymentCards");
        }
        // GET: PaymentCardsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentCardsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PaymentCardsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PaymentCardsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PaymentCardsController/Delete/5
        public ActionResult Delete(int IdCard)
        {
            PaymentCard oCard = _DBContext.PaymentCards.Include(o => o.oUsers).Where(x => x.IdCard == IdCard).FirstOrDefault();
            return View(oCard);
        }

        // POST: PaymentCardsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PaymentCard oCard)
        {
            try
            {
                _DBContext.PaymentCards.Remove(oCard);
                _DBContext.SaveChanges();
                return RedirectToAction("Index", "PaymentCards");
            }
            catch
            {
                return View();
            }
        }
    }
}
