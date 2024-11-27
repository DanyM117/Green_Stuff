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
        // GET: AddressesController/Detail/0 para crear una nueva dirección
        public ActionResult Detail(int IdAdd = 0)
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
                // Editar dirección existente
                oAddVM.oAddress = _DBContext.Addresses.Find(IdAdd);
                if (oAddVM.oAddress == null)
                {
                    TempData["ErrorMessage"] = "Dirección no encontrada.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                // Crear nueva dirección: asignar la fecha actual
                oAddVM.oAddress.ModificationDate = DateTime.Now;
                oAddVM.oAddress.Enabled = true; // Valor predeterminado (opcional)
            }

            return View(oAddVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Detail(AddressesVM oAddVM)
        {
            if (ModelState.IsValid)
            {
                if (oAddVM.oAddress.IdAdd == 0)
                {
                    // Crear nueva dirección
                    _DBContext.Addresses.Add(oAddVM.oAddress);
                    TempData["SuccessMessage"] = "Dirección creada exitosamente.";
                }
                else
                {
                    // Editar dirección existente
                    var existingAddress = _DBContext.Addresses.Find(oAddVM.oAddress.IdAdd);
                    if (existingAddress != null)
                    {
                        existingAddress.Iduser = oAddVM.oAddress.Iduser; // Asegurarse de mantener el IdUser
                        existingAddress.Name = oAddVM.oAddress.Name;
                        existingAddress.ModificationDate = DateTime.Now; // Actualizar fecha de modificación
                        existingAddress.Enabled = oAddVM.oAddress.Enabled;

                        _DBContext.Addresses.Update(existingAddress);
                        TempData["SuccessMessage"] = "Dirección actualizada exitosamente.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Dirección no encontrada.";
                        return RedirectToAction("Index");
                    }
                }
                _DBContext.SaveChanges();
                return RedirectToAction("Index", "Addresses");
            }
            else
            {
                TempData["ErrorMessage"] = "Por favor, corrige los errores en el formulario.";
                // Volver a cargar la lista de usuarios en caso de error
                oAddVM.oUserList = _DBContext.Users.Select(users => new SelectListItem()
                {
                    Text = users.Username,
                    Value = users.IdUser.ToString()
                }).ToList();
                return View(oAddVM);
            }
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
