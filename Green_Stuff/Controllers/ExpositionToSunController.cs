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
                // Editar exposición al sol existente
                oSunVM.oSun = _DBContext.ExpositionToSuns.Find(IdSun);
                if (oSunVM.oSun == null)
                {
                    TempData["ErrorMessage"] = "Exposición al sol no encontrada.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                // Crear nueva exposición al sol: asignar la fecha actual
                oSunVM.oSun.ModificationDate = DateTime.Now;
                oSunVM.oSun.Enabled = true; // Valor predeterminado (opcional)
            }

            return View(oSunVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Detail(ExpositionToSunVM oSunVM)
        {
            if (ModelState.IsValid)
            {
                if (oSunVM.oSun.IdSun == 0)
                {
                    // Crear nueva exposición al sol
                    _DBContext.ExpositionToSuns.Add(oSunVM.oSun);
                    TempData["SuccessMessage"] = "Exposición al sol creada exitosamente.";
                }
                else
                {
                    // Editar exposición al sol existente
                    var existingSun = _DBContext.ExpositionToSuns.Find(oSunVM.oSun.IdSun);
                    if (existingSun != null)
                    {
                        existingSun.Name = oSunVM.oSun.Name;
                        existingSun.Description = oSunVM.oSun.Description;
                        existingSun.Enabled = oSunVM.oSun.Enabled;
                        existingSun.ModificationDate = DateTime.Now; // Actualizar fecha de modificación

                        _DBContext.ExpositionToSuns.Update(existingSun);
                        TempData["SuccessMessage"] = "Exposición al sol actualizada exitosamente.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Exposición al sol no encontrada.";
                        return RedirectToAction("Index");
                    }
                }
                _DBContext.SaveChanges();
                return RedirectToAction("Index", "ExpositionToSun");
            }
            else
            {
                TempData["ErrorMessage"] = "Por favor, corrige los errores en el formulario.";
                return View(oSunVM);
            }
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
