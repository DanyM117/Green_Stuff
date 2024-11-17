using Green_Stuff.Models;
using Green_Stuff.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Green_Stuff.Controllers
{
    public class SizesController : Controller
    {
        private readonly DbLabpwebContext _DBContext;
        public SizesController(DbLabpwebContext context)
        {
            _DBContext = context;
        }
        // GET: SizesController
        public ActionResult Index()
        {
            List<Size> sizes = _DBContext.Sizes.ToList();
            return View(sizes);
        }
        [HttpGet]
        // GET: SizesController/Details/5
        public ActionResult Detail(int IdSize)
        {
            SizesVM oSizesVM = new SizesVM()
            {
                oSize = new Size()
            };
            if(IdSize != 0)
            {
                oSizesVM.oSize = _DBContext.Sizes.Find(IdSize);
            }
            return View(oSizesVM);
        }

        [HttpPost]
        public ActionResult Detail(SizesVM oSizeVM)
        {
            if (oSizeVM.oSize.IdSize == 0)
            {
                _DBContext.Sizes.Add(oSizeVM.oSize);
            }
            else
            {
                _DBContext.Sizes.Update(oSizeVM.oSize);
            }
            _DBContext.SaveChanges();
            return RedirectToAction("Index", "Sizes");
        }
        // GET: SizesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SizesController/Create
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

        // GET: SizesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SizesController/Edit/5
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
        // GET: SizesController/Delete/5
        public ActionResult Delete(int IdSize)
        {
            Size oSize = _DBContext.Sizes.Where(x => x.IdSize == IdSize).FirstOrDefault();
            return View(oSize);
        }
        // POST: SizesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Size oSize)
        {
            try
            {
                _DBContext.Sizes.Remove(oSize);
                _DBContext.SaveChanges();
                return RedirectToAction("Index","Sizes");
            }
            catch
            {
                return View();
            }
        }
    }
}
