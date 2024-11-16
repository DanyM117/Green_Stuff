using Green_Stuff.Models;
using Green_Stuff.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Green_Stuff.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly DbLabpwebContext _DBContext;
        public WarehouseController(DbLabpwebContext context)
        {
            _DBContext = context;
        }
        // GET: WarehouseController
        public ActionResult Index()
        {
            List<Warehouse> listwarehouse = _DBContext.Warehouses.Include(o => o.oSizes)
                .Include(o => o.oExpositionToSun).Include(o => o.oWaterDemmand).Include(o => o.oCategories).ToList();
            return View(listwarehouse);
        }

        // GET: WarehouseController/Details/5
        [HttpGet]
        public ActionResult Warehouse_Detail(int IdItem)
        {
            WarehouseVM oWarehouseVM = new WarehouseVM()
            {
                oWarehouse = new Warehouse(),
                oSizesList = _DBContext.Sizes.Select(sizes => new SelectListItem()
                {
                    Text = sizes.Name,
                    Value = sizes.IdSize.ToString()
                }).ToList(),
                oExpositionToSunList = _DBContext.ExpositionToSuns.Select(sun => new SelectListItem()
                {
                    Text = sun.Name,
                    Value = sun.IdSun.ToString()
                }).ToList(),
                oWaterDemmandList = _DBContext.WaterDemmands.Select(water => new SelectListItem()
                {
                    Text = water.Name,
                    Value = water.IdWater.ToString()
                }).ToList(),
                oCategoriesList = _DBContext.Categories.Select(categories => new SelectListItem()
                {
                    Text = categories.Name,
                    Value = categories.IdCategory.ToString()
                }).ToList()
            };
            if(IdItem != 0)
            {
                oWarehouseVM.oWarehouse = _DBContext.Warehouses.Find(IdItem);
            }
            return View(oWarehouseVM);
        }
        [HttpPost]
        public ActionResult Warehouse_Detail(WarehouseVM oWarehouseVM)
        {
            if(oWarehouseVM.oWarehouse.IdItem == 0)
            {
                _DBContext.Warehouses.Add(oWarehouseVM.oWarehouse);
            }
            else
            {
                _DBContext.Warehouses.Update(oWarehouseVM.oWarehouse);
            }
            _DBContext.SaveChanges();
            return RedirectToAction("Index","Warehouse");
        }

        // GET: WarehouseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WarehouseController/Create
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

        // GET: WarehouseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WarehouseController/Edit/5
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
        // GET: WarehouseController/Delete/5
        public ActionResult Delete(int IdItem)
        {
            Warehouse oWarehouse = _DBContext.Warehouses.Include(o => o.oSizes)
                .Include(o => o.oExpositionToSun).Include(o => o.oWaterDemmand).Include(o => o.oCategories).Where(x => x.IdItem == IdItem).FirstOrDefault();
            return View(oWarehouse);
        }

        // POST: WarehouseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Warehouse oWarehouse)
        {
            try
            {
                _DBContext.Warehouses.Remove(oWarehouse);
                _DBContext.SaveChanges();
                return RedirectToAction("Index","Warehouse");
            }
            catch
            {
                return View();
            }
        }
    }
}
