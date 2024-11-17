using Green_Stuff.Models;
using Green_Stuff.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Green_Stuff.Controllers
{
    public class ExpositionToSunController : Controller
    {
        private readonly DbLabpwebContext _DBContext;
        public ExpositionToSunController(DbLabpwebContext context)
        {
            _DBContext = context;
        }
        // GET: ExpositionToSunController
        public ActionResult Index()
        {
            List<ExpositionToSun> sun = _DBContext.ExpositionToSuns.ToList();
            return View(sun);
        }
        [HttpGet]
        // GET: ExpositionToSunController/Details/5
        public ActionResult Detail(int IdSun)
        {
            ExpositionToSunVM oSunVM = new ExpositionToSunVM()
            {
                oSun = new ExpositionToSun()
            };
            if (IdSun != 0)
            {
                oSunVM.oSun = _DBContext.ExpositionToSuns.Find(IdSun);
            }
            return View(oSunVM);
        }
        [HttpPost]
        public ActionResult Detail(ExpositionToSunVM oSunVM)
        {
            if (oSunVM.oSun.IdSun == 0)
            {
                _DBContext.ExpositionToSuns.Add(oSunVM.oSun);
            }
            else
            {
                _DBContext.ExpositionToSuns.Update(oSunVM.oSun);
            }
            _DBContext.SaveChanges();
            return RedirectToAction("Index", "ExpositionToSun");
        }

        // GET: ExpositionToSunController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpositionToSunController/Create
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

        // GET: ExpositionToSunController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExpositionToSunController/Edit/5
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
        // GET: ExpositionToSunController/Delete/5
        public ActionResult Delete(int IdSun)
        {
            ExpositionToSun oSun = _DBContext.ExpositionToSuns.Where(x => x.IdSun == IdSun).FirstOrDefault();
            return View(oSun);
        }

        // POST: ExpositionToSunController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ExpositionToSun oSun)
        {
            try
            {
                _DBContext.ExpositionToSuns.Remove(oSun);
                _DBContext.SaveChanges();
                return RedirectToAction("Index", "ExpositionToSun");
            }
            catch
            {
                return View();
            }
        }
    }
}
