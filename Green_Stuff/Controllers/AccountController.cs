using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Green_Stuff.Models;
using System.Threading.Tasks;

namespace Green_Stuff.Controllers
{
    public class AccountController : Controller
    {
        private readonly DbLabpwebContext _context;

        public AccountController(DbLabpwebContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> EditProfile()
        {
            var username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Acceso");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(User model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(model.IdUser);

                if (user == null)
                {
                    return NotFound();
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Username = model.Username;

                if (!string.IsNullOrEmpty(model.Password))
                {
                    user.Password = model.Password;
                }


                _context.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public async Task<IActionResult> PaymentCards()
        {
            var userId = HttpContext.Session.GetInt32("UserID");

            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Acceso");
            }

            var cards = await _context.PaymentCards.Where(c => c.Iduser == userId.Value).ToListAsync();

            return View(cards);
        }

        [HttpGet]
        public IActionResult AddCard()
        {
            return View(new PaymentCard());
        }

        [HttpPost]
        public async Task<IActionResult> AddCard(PaymentCard model)
        {
            var userId = HttpContext.Session.GetInt32("UserID");

            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Acceso");
            }

            if (ModelState.IsValid)
            {
                model.Iduser = userId.Value;
                _context.PaymentCards.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("PaymentCards");
            }

            return View(model);
        }
    }
}
