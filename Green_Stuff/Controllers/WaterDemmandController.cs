using Green_Stuff.Models;
using Green_Stuff.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Green_Stuff.Controllers
{
    public class WaterDemmandController : Controller
    {
        private readonly DbLabpwebContext _DBContext;
        public WaterDemmandController(DbLabpwebContext context)
        {
            _DBContext = context;
        }
        [HttpGet]
        // GET: WaterDemmandController
        public ActionResult Index()
        {
            List<WaterDemmand> water = _DBContext.WaterDemmands.ToList();
            return View(water);
        }
        [HttpGet]
        // GET: WaterDemmandController/Details/5
        public ActionResult Detail(int IdWater)
        {
            WaterDemmandVM oWaterVM = new WaterDemmandVM()
            {
                oWater = new WaterDemmand()
            };

            if (IdWater != 0)
            {
                // Editar demanda de agua existente
                oWaterVM.oWater = _DBContext.WaterDemmands.Find(IdWater);
                if (oWaterVM.oWater == null)
                {
                    TempData["ErrorMessage"] = "Demanda de agua no encontrada.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                // Crear nueva demanda de agua: asignar la fecha actual
                oWaterVM.oWater.ModificationDate = DateTime.Now;
                oWaterVM.oWater.Enabled = true; // Valor predeterminado (opcional)
            }

            return View(oWaterVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Detail(WaterDemmandVM oWaterVM)
        {
            if (ModelState.IsValid)
            {
                if (oWaterVM.oWater.IdWater == 0)
                {
                    // Crear nueva demanda de agua
                    _DBContext.WaterDemmands.Add(oWaterVM.oWater);
                    TempData["SuccessMessage"] = "Demanda de agua creada exitosamente.";
                }
                else
                {
                    // Editar demanda de agua existente
                    var existingWater = _DBContext.WaterDemmands.Find(oWaterVM.oWater.IdWater);
                    if (existingWater != null)
                    {
                        existingWater.Name = oWaterVM.oWater.Name;
                        existingWater.Description = oWaterVM.oWater.Description;
                        existingWater.Enabled = oWaterVM.oWater.Enabled;
                        existingWater.ModificationDate = DateTime.Now; // Actualizar fecha de modificación

                        _DBContext.WaterDemmands.Update(existingWater);
                        TempData["SuccessMessage"] = "Demanda de agua actualizada exitosamente.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Demanda de agua no encontrada.";
                        return RedirectToAction("Index");
                    }
                }
                _DBContext.SaveChanges();
                return RedirectToAction("Index", "WaterDemmand");
            }
            else
            {
                TempData["ErrorMessage"] = "Por favor, corrige los errores en el formulario.";
                return View(oWaterVM);
            }
        }
        // GET: WaterDemmandController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WaterDemmandController/Create
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

        // GET: WaterDemmandController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WaterDemmandController/Edit/5
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
        // GET: WaterDemmandController/Delete/5
        public ActionResult Delete(int IdWater)
        {
            WaterDemmand oWater = _DBContext.WaterDemmands.Where(x => x.IdWater == IdWater).FirstOrDefault();
            return View(oWater);
        }

        // POST: WaterDemmandController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(WaterDemmand oWater)
        {
            try
            {
                _DBContext.WaterDemmands.Remove(oWater);
                _DBContext.SaveChanges();
                return RedirectToAction("Index", "WaterDemmand");
            }
            catch
            {
                return View();
            }
        }
    }
}
