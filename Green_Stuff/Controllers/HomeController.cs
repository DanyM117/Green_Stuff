using System.Diagnostics;
using Green_Stuff.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;


namespace Green_Stuff.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbLabpwebContext _context;

        public HomeController(DbLabpwebContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var advertisements = _context.Advertisements.ToList();
            return View(advertisements);
        }
    }
}
