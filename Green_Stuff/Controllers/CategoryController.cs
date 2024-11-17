using Green_Stuff.Models;
using Green_Stuff.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Green_Stuff.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DbLabpwebContext _DBContext;
        public CategoryController(DbLabpwebContext context)
        {
            _DBContext = context;
        }
        // GET: CategoryController
        public ActionResult Index()
        {
            List<Category> cat = _DBContext.Categories.ToList();
            return View(cat);
        }
        [HttpGet]
        // GET: CategoryController/Details/5
        public ActionResult Detail(int IdCategory)
        {
            CategoryVM oCatVM = new CategoryVM()
            {
                oCategory = new Category()
            };
            if (IdCategory != 0)
            {
                oCatVM.oCategory = _DBContext.Categories.Find(IdCategory);
            }
            return View(oCatVM);
        }

        [HttpPost]
        public ActionResult Detail(CategoryVM oCatVM)
        {
            if (oCatVM.oCategory.IdCategory == 0)
            {
                _DBContext.Categories.Add(oCatVM.oCategory);
            }
            else
            {
                _DBContext.Categories.Update(oCatVM.oCategory);
            }
            _DBContext.SaveChanges();
            return RedirectToAction("Index", "Category");
        }
        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
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

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoryController/Edit/5
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
        // GET: CategoryController/Delete/5
        public ActionResult Delete(int IdCategory)
        {
            Category oCategory = _DBContext.Categories.Where(x => x.IdCategory == IdCategory).FirstOrDefault();
            return View(oCategory);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Category oCategory)
        {
            try
            {
                _DBContext.Categories.Remove(oCategory);
                _DBContext.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            catch
            {
                return View();
            }
        }
    }
}
