using Green_Stuff.Models;
using Green_Stuff.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Green_Stuff.Controllers
{
    public class WaterDemmandController : Controller
    {
        private readonly DbLabpwebContext _DBContext;
        public WaterDemmandController(DbLabpwebContext context)
        {
            _DBContext = context;
        }
        [HttpGet]
        // GET: WaterDemmandController
        public ActionResult Index()
        {
            List<WaterDemmand> water = _DBContext.WaterDemmands.ToList();
            return View(water);
        }
        [HttpGet]
        // GET: WaterDemmandController/Details/5
        public ActionResult Detail(int IdWater)
        {
            WaterDemmandVM oWaterVM = new WaterDemmandVM()
            {
                oWater = new WaterDemmand()
            };
            if (IdWater != 0)
            {
                oWaterVM.oWater = _DBContext.WaterDemmands.Find(IdWater);
            }
            return View(oWaterVM);
        }
        [HttpPost]
        public ActionResult Detail(WaterDemmandVM oWaterWM)
        {
            if (oWaterWM.oWater.IdWater == 0)
            {
                _DBContext.WaterDemmands.Add(oWaterWM.oWater);
            }
            else
            {
                _DBContext.WaterDemmands.Update(oWaterWM.oWater);
            }
            _DBContext.SaveChanges();
            return RedirectToAction("Index", "WaterDemmand");
        }
        // GET: WaterDemmandController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WaterDemmandController/Create
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

        // GET: WaterDemmandController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WaterDemmandController/Edit/5
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
        // GET: WaterDemmandController/Delete/5
        public ActionResult Delete(int IdWater)
        {
            WaterDemmand oWater = _DBContext.WaterDemmands.Where(x => x.IdWater == IdWater).FirstOrDefault();
            return View(oWater);
        }

        // POST: WaterDemmandController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(WaterDemmand oWater)
        {
            try
            {
                _DBContext.WaterDemmands.Remove(oWater);
                _DBContext.SaveChanges();
                return RedirectToAction("Index", "WaterDemmand");
            }
            catch
            {
                return View();
            }
        }
    }
}
