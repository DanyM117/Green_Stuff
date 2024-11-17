using Green_Stuff.Models;
using Green_Stuff.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Green_Stuff.Controllers
{
    public class AdvertisementsController : Controller
    {
        private readonly DbLabpwebContext _DBContext;
        public AdvertisementsController(DbLabpwebContext context)
        {
            _DBContext = context;
        }
        // GET: AdvertisementsController
        public ActionResult Index()
        {
            List<Advertisement> listad = _DBContext.Advertisements.Include(o => o.oUsers).ToList();
            return View(listad);
        }
        [HttpGet]
        // GET: AdvertisementsController/Details/5
        public ActionResult Detail(int IdAdvertisement)
        {
            AdvertisementsVM oAdVM= new AdvertisementsVM()
            {
                oAd = new Advertisement(),
                oadslist = _DBContext.Users.Select(users => new SelectListItem()
                {
                    Text = users.Username,
                    Value = users.IdUser.ToString()
                }).ToList()
            };
            if (IdAdvertisement != 0)
            {
                oAdVM.oAd = _DBContext.Advertisements.Find(IdAdvertisement);
            }
            return View(oAdVM);
        }
        [HttpPost]
        public ActionResult Detail(AdvertisementsVM oAdVM)
        {
            if (oAdVM.oAd.IdAdvertisement == 0)
            {
                _DBContext.Advertisements.Add(oAdVM.oAd);
            }
            else
            {
                _DBContext.Advertisements.Update(oAdVM.oAd);
            }
            _DBContext.SaveChanges();
            return RedirectToAction("Index", "Advertisements");
        }

        // GET: AdvertisementsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdvertisementsController/Create
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

        // GET: AdvertisementsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdvertisementsController/Edit/5
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
        [HttpGet]
        // GET: AdvertisementsController/Delete/5
        public ActionResult Delete(int IdAdvertisement)
        {
            Advertisement oAdvertisement = _DBContext.Advertisements.Include(o => o.oUsers).Where(x => x.IdAdvertisement == IdAdvertisement).FirstOrDefault();
            return View(oAdvertisement);
        }

        // POST: AdvertisementsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Advertisement oAdvertisement)
        {
            try
            {
                _DBContext.Advertisements.Remove(oAdvertisement);
                _DBContext.SaveChanges();
                return RedirectToAction("Index", "Advertisements");
            }
            catch
            {
                return View();
            }
        }
    }
}
