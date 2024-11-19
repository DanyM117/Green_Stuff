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
        // GET: AdvertisementsController/Detail/5
        public ActionResult Detail(int IdAdvertisement)
        {
            AdvertisementsVM oAdVM = new AdvertisementsVM()
            {
                oAd = new Advertisement(),
                Username = ""
            };

            if (IdAdvertisement != 0)
            {
                oAdVM.oAd = _DBContext.Advertisements.Include(a => a.oUsers).FirstOrDefault(a => a.IdAdvertisement == IdAdvertisement);
                if (oAdVM.oAd == null)
                {
                    return NotFound();
                }

                oAdVM.Username = oAdVM.oAd.oUsers.Username;
            }
            else
            {
                oAdVM.Username = HttpContext.Session.GetString("Username");
            }

            return View(oAdVM);
        }


        [HttpPost]
        public ActionResult Detail(AdvertisementsVM oAdVM)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Login", "Acceso");
            }

            if (oAdVM.oAd.IdAdvertisement == 0)
            {
                oAdVM.oAd.Iduser = userId.Value;

                _DBContext.Advertisements.Add(oAdVM.oAd);
            }
            else
            {
                var existingAd = _DBContext.Advertisements.Find(oAdVM.oAd.IdAdvertisement);
                if (existingAd == null)
                {
                    return NotFound();
                }

                existingAd.Title = oAdVM.oAd.Title;
                existingAd.Description = oAdVM.oAd.Description;
                existingAd.ImagePath = oAdVM.oAd.ImagePath;

                _DBContext.Advertisements.Update(existingAd);
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
