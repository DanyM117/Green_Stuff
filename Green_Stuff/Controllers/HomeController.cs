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
        private readonly DbLabpwebContext _DBContext;

        public HomeController(DbLabpwebContext context)
        {
            _DBContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
