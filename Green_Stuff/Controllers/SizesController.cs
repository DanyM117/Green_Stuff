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
        public ActionResult Detail(int IdSize = 0)
        {
            SizesVM oSizesVM = new SizesVM()
            {
                oSize = new Size()
            };

            if (IdSize != 0)
            {
                // Editar tamaño existente
                oSizesVM.oSize = _DBContext.Sizes.Find(IdSize);
                if (oSizesVM.oSize == null)
                {
                    TempData["ErrorMessage"] = "Tamaño no encontrado.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                // Crear nuevo tamaño: asignar la fecha actual
                oSizesVM.oSize.ModificationDate = DateTime.Now;
                oSizesVM.oSize.Enabled = true; // Valor predeterminado (opcional)
            }

            return View(oSizesVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Detail(SizesVM oSizeVM)
        {
            if (ModelState.IsValid)
            {
                if (oSizeVM.oSize.IdSize == 0)
                {
                    // Crear nuevo tamaño
                    _DBContext.Sizes.Add(oSizeVM.oSize);
                    TempData["SuccessMessage"] = "Tamaño creado exitosamente.";
                }
                else
                {
                    // Editar tamaño existente
                    var existingSize = _DBContext.Sizes.Find(oSizeVM.oSize.IdSize);
                    if (existingSize != null)
                    {
                        existingSize.Name = oSizeVM.oSize.Name;
                        existingSize.Description = oSizeVM.oSize.Description;
                        existingSize.Enabled = oSizeVM.oSize.Enabled;
                        existingSize.ModificationDate = DateTime.Now; // Actualizar fecha de modificación

                        _DBContext.Sizes.Update(existingSize);
                        TempData["SuccessMessage"] = "Tamaño actualizado exitosamente.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Tamaño no encontrado.";
                        return RedirectToAction("Index");
                    }
                }
                _DBContext.SaveChanges();
                return RedirectToAction("Index", "Sizes");
            }
            else
            {
                TempData["ErrorMessage"] = "Por favor, corrige los errores en el formulario.";
                return View(oSizeVM);
            }
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
