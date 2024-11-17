using Green_Stuff.Models;
using Green_Stuff.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Green_Stuff.Controllers
{
    public class UserController : Controller
    {
        private readonly DbLabpwebContext _DBContext;
        public UserController(DbLabpwebContext context)
        {
            _DBContext = context;
        }
        // GET: UserController
        public ActionResult Index()
        {
            List<User> listuser = _DBContext.Users.Include(o => o.oUserTypes).ToList();
            return View(listuser);
        }
        [HttpGet]
        // GET: UserController/Details/5
        public ActionResult Detail(int IdUser)
        {
            UsersVM oUsersVM = new UsersVM()
            {
                oUser = new User(),
                oUserTypeList = _DBContext.UserTypes.Select(users => new SelectListItem()
                {
                    Text = users.Name,
                    Value = users.IdUserType.ToString()
                }).ToList()
            };
            if (IdUser != 0)
            {
                oUsersVM.oUser = _DBContext.Users.Find(IdUser);
            }
            return View(oUsersVM);
        }
        [HttpPost]
        public ActionResult Detail(UsersVM oUsersVM)
        {
            if (oUsersVM.oUser.IdUser == 0)
            {
                _DBContext.Users.Add(oUsersVM.oUser);
            }
            else
            {
                _DBContext.Users.Update(oUsersVM.oUser);
            }
            _DBContext.SaveChanges();
            return RedirectToAction("Index", "User");
        }
        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
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

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
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
        // GET: UserController/Delete/5
        public ActionResult Delete(int IdUser)
        {
            User oUser = _DBContext.Users.Include(o => o.oUserTypes).Where(x => x.IdUser == IdUser).FirstOrDefault();
            return View(oUser);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(User oUser)
        {
            try
            {
                _DBContext.Users.Remove(oUser);
                _DBContext.SaveChanges();
                return RedirectToAction("Index", "User");
            }
            catch
            {
                return View();
            }
        }
    }
}
