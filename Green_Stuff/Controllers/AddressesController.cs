using Green_Stuff.Models;
using Green_Stuff.Models.ViewModels;
//using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Green_Stuff.Controllers
{
    public class AddressesController : Controller
    {
        private readonly DbLabpwebContext _DBContext;
        public AddressesController(DbLabpwebContext context)
        {
            _DBContext = context;
        }
        // GET: AddressesController
        public ActionResult Index()
        {
            List<Address> listaddress= _DBContext.Addresses.Include(o => o.oUsers).ToList();
            return View(listaddress);
        }
        [HttpGet]
        // GET: AddressesController/Details/5
        public ActionResult Detail(int IdAdd)
        {
            AddressesVM oAddVM = new AddressesVM()
            {
                oAddress = new Address(),
                oUserList = _DBContext.Users.Select(users => new SelectListItem()
                {
                    Text = users.Username,
                    Value = users.IdUser.ToString()
                }).ToList()
            };
            if (IdAdd != 0)
            {
                oAddVM.oAddress = _DBContext.Addresses.Find(IdAdd);
            }
            return View(oAddVM);
        }
        [HttpPost]
        public ActionResult Detail(AddressesVM oAddVM)
        {
            if (oAddVM.oAddress.IdAdd == 0)
            {
                _DBContext.Addresses.Add(oAddVM.oAddress);
            }
            else
            {
                _DBContext.Addresses.Update(oAddVM.oAddress);
            }
            _DBContext.SaveChanges();
            return RedirectToAction("Index", "Addresses");
        }

        // GET: AddressesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AddressesController/Create
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

        // GET: AddressesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AddressesController/Edit/5
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
        // GET: AddressesController/Delete/5
        public ActionResult Delete(int IdAdd)
        {
            Address oAddress = _DBContext.Addresses.Include(o => o.oUsers).Where(x => x.IdAdd == IdAdd).FirstOrDefault();
            return View(oAddress);
        }

        // POST: AddressesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Address oAddress)
        {
            try
            {
                _DBContext.Addresses.Remove(oAddress);
                _DBContext.SaveChanges();
                return RedirectToAction("Index", "Addresses");
            }
            catch
            {
                return View();
            }
        }
    }
}
