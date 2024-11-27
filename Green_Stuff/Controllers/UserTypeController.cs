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
        // GET: UserTypeController/Detail/0 para crear un nuevo tipo de usuario
        public ActionResult Detail(int IdUserType = 0)
        {
            UserTypeVM oUserTypeVM = new UserTypeVM()
            {
                oUserType = new UserType()
            };

            if (IdUserType != 0)
            {
                // Editar tipo de usuario existente
                oUserTypeVM.oUserType = _DBContext.UserTypes.Find(IdUserType);
                if (oUserTypeVM.oUserType == null)
                {
                    TempData["ErrorMessage"] = "Tipo de usuario no encontrado.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                // Crear nuevo tipo de usuario: asignar la fecha actual
                oUserTypeVM.oUserType.ModificationDate = DateTime.Now;
                oUserTypeVM.oUserType.Enabled = true; // Valor predeterminado (opcional)
            }

            return View(oUserTypeVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Detail(UserTypeVM oUserTypeVM)
        {
            if (ModelState.IsValid)
            {
                if (oUserTypeVM.oUserType.IdUserType == 0)
                {
                    // Crear nuevo tipo de usuario
                    _DBContext.UserTypes.Add(oUserTypeVM.oUserType);
                    TempData["SuccessMessage"] = "Tipo de usuario creado exitosamente.";
                }
                else
                {
                    // Editar tipo de usuario existente
                    var existingUserType = _DBContext.UserTypes.Find(oUserTypeVM.oUserType.IdUserType);
                    if (existingUserType != null)
                    {
                        existingUserType.Name = oUserTypeVM.oUserType.Name;
                        existingUserType.ModificationDate = DateTime.Now; // Actualizar fecha de modificación
                        existingUserType.Enabled = oUserTypeVM.oUserType.Enabled;

                        _DBContext.UserTypes.Update(existingUserType);
                        TempData["SuccessMessage"] = "Tipo de usuario actualizado exitosamente.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Tipo de usuario no encontrado.";
                        return RedirectToAction("Index");
                    }
                }
                _DBContext.SaveChanges();
                return RedirectToAction("Index", "UserType");
            }
            else
            {
                TempData["ErrorMessage"] = "Por favor, corrige los errores en el formulario.";
                return View(oUserTypeVM);
            }
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
