using Green_Stuff.Models;
using Green_Stuff.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Green_Stuff.Controllers
{
    public class AdvertisementsController : Controller
    {
        private readonly DbLabpwebContext _DBContext;
        public AdvertisementsController(DbLabpwebContext context)
        {
            _DBContext = context;
        }
        // GET: AdvertisementsController
        public ActionResult Index()
        {
            List<Advertisement> listad = _DBContext.Advertisements.Include(o => o.oUsers).ToList();
            return View(listad);
        }
        /*[HttpGet]
        // GET: AdvertisementsController/Details/5
        public ActionResult Detail(int IdAdvertisement)
        {
            AdvertisementsVM oAdVM= new AdvertisementsVM()
            {
                oAd = new Advertisement(),
                oadslist = _DBContext.Users.Select(users => new SelectListItem()
                {
                    Text = users.Username,
                    Value = users.IdUser.ToString()
                }).ToList()
            };
            if (IdAdvertisement != 0)
            {
                oAdVM.oAd = _DBContext.Advertisements.Find(IdAdvertisement);
            }
            return View(oAdVM);
        }*/
        [HttpGet]
        // GET: AdvertisementsController/Detail/5
        public ActionResult Detail(int IdAdvertisement)
        {
            AdvertisementsVM oAdVM = new AdvertisementsVM()
            {
                oAd = new Advertisement(),
                Username = "" // Inicializar el nombre de usuario
            };

            if (IdAdvertisement != 0)
            {
                // Editando anuncio existente
                oAdVM.oAd = _DBContext.Advertisements.Include(a => a.oUsers).FirstOrDefault(a => a.IdAdvertisement == IdAdvertisement);
                if (oAdVM.oAd == null)
                {
                    return NotFound();
                }

                // Obtener el nombre de usuario del creador original
                oAdVM.Username = oAdVM.oAd.oUsers.Username;
            }
            else
            {
                // Creando nuevo anuncio
                // Obtener el nombre de usuario actual de la sesión
                oAdVM.Username = HttpContext.Session.GetString("Username");
            }

            return View(oAdVM);
        }


        /*[HttpPost]
        public ActionResult Detail(AdvertisementsVM oAdVM)
        {
            if (oAdVM.oAd.IdAdvertisement == 0)
            {
                _DBContext.Advertisements.Add(oAdVM.oAd);
            }
            else
            {
                _DBContext.Advertisements.Update(oAdVM.oAd);
            }
            _DBContext.SaveChanges();
            return RedirectToAction("Index", "Advertisements");
        }*/
        [HttpPost]
        public ActionResult Detail(AdvertisementsVM oAdVM)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                // Si el usuario no ha iniciado sesión, redirigir al login
                return RedirectToAction("Login", "Acceso");
            }

            if (oAdVM.oAd.IdAdvertisement == 0)
            {
                // Crear nuevo anuncio
                // Asignar el ID del usuario actual como creador
                oAdVM.oAd.Iduser = userId.Value;

                _DBContext.Advertisements.Add(oAdVM.oAd);
            }
            else
            {
                // Editar anuncio existente
                // Buscar el anuncio existente en la base de datos
                var existingAd = _DBContext.Advertisements.Find(oAdVM.oAd.IdAdvertisement);
                if (existingAd == null)
                {
                    return NotFound();
                }

                // Actualizar los campos necesarios sin modificar el Iduser
                existingAd.Title = oAdVM.oAd.Title;
                existingAd.Description = oAdVM.oAd.Description;
                existingAd.ImagePath = oAdVM.oAd.ImagePath;

                _DBContext.Advertisements.Update(existingAd);
            }

            _DBContext.SaveChanges();
            return RedirectToAction("Index", "Advertisements");
        }


        // GET: AdvertisementsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdvertisementsController/Create
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

        // GET: AdvertisementsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdvertisementsController/Edit/5
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
        // GET: AdvertisementsController/Delete/5
        public ActionResult Delete(int IdAdvertisement)
        {
            Advertisement oAdvertisement = _DBContext.Advertisements.Include(o => o.oUsers).Where(x => x.IdAdvertisement == IdAdvertisement).FirstOrDefault();
            return View(oAdvertisement);
        }


        // POST: AdvertisementsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Advertisement oAdvertisement)
        {
            try
            {
                _DBContext.Advertisements.Remove(oAdvertisement);
                _DBContext.SaveChanges();
                return RedirectToAction("Index", "Advertisements");
            }
            catch
            {
                return View();
            }
        }
    }
}
