using Green_Stuff.Models;
using Green_Stuff.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Green_Stuff.Controllers
{
    public class UserTypeController : Controller
    {
        private readonly DbLabpwebContext _DBContext;
        public UserTypeController(DbLabpwebContext context)
        {
            _DBContext = context;
        }
        // GET: UserTypeController
        public ActionResult Index()
        {
            List<UserType> usert = _DBContext.UserTypes.ToList();
            return View(usert);
        }
        [HttpGet]
        // GET: UserTypeController/Details/5
        public ActionResult Detail(int IdUserType)
        {
            UserTypeVM oUserTypeVM = new UserTypeVM()
            {
                oUserType = new UserType()
            };
            if (IdUserType != 0)
            {
                oUserTypeVM.oUserType = _DBContext.UserTypes.Find(IdUserType);
            }
            return View(oUserTypeVM);
        }
        [HttpPost]
        public ActionResult Detail(UserTypeVM oUserTypeVM)
        {
            if (oUserTypeVM.oUserType.IdUserType == 0)
            {
                _DBContext.UserTypes.Add(oUserTypeVM.oUserType);
            }
            else
            {
                _DBContext.UserTypes.Update(oUserTypeVM.oUserType);
            }
            _DBContext.SaveChanges();
            return RedirectToAction("Index", "UserType");
        }

        // GET: UserTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserTypeController/Create
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

        // GET: UserTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserTypeController/Edit/5
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
        // GET: UserTypeController/Delete/5
        public ActionResult Delete(int IdUserType)
        {
            UserType oUserType = _DBContext.UserTypes.Where(x => x.IdUserType == IdUserType).FirstOrDefault();
            return View(oUserType);
        }

        // POST: UserTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UserType oUserType)
        {
            try
            {
                _DBContext.UserTypes.Remove(oUserType);
                _DBContext.SaveChanges();
                return RedirectToAction("Index", "UserType");
            }
            catch
            {
                return View();
            }
        }
    }
}
