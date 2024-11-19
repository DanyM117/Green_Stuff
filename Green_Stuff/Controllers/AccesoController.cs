using Microsoft.AspNetCore.Mvc;
using Green_Stuff.Models;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Green_Stuff.Controllers
{
    public class AccesoController : Controller
    {
        private readonly string cadenaSQL;
        private readonly DbLabpwebContext _DBContext;
        public AccesoController(IConfiguration config, DbLabpwebContext context)
        {
            cadenaSQL = config.GetConnectionString("cadenaSQL");
            _DBContext = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(User oUser)
        {

            bool Registrado;
            string Mensaje;

            using (SqlConnection cn = new SqlConnection(cadenaSQL))
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", cn);
                cmd.Parameters.AddWithValue("IDUserType", 2); // Usuario tipo Cliente
                cmd.Parameters.AddWithValue("Username", oUser.Username);
                cmd.Parameters.AddWithValue("FirstName", oUser.FirstName);
                cmd.Parameters.AddWithValue("LastName", oUser.LastName);
                cmd.Parameters.AddWithValue("Email", oUser.Email);
                cmd.Parameters.AddWithValue("password_", oUser.Password);
                cmd.Parameters.AddWithValue("ImagePath", oUser.ImagePath);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                cmd.ExecuteNonQuery();
                Registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
            }

            ViewData["Mensaje"] = Mensaje;
            if (Registrado)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                return View(oUser);
            }
        }
        [HttpPost]
        public ActionResult ValidarUser(string userorEmail, string password)
        {
            if (string.IsNullOrEmpty(userorEmail) || string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Login", "Acceso");
            }

            var user = _DBContext.Users
                       .Include(u => u.oUserTypes)
                       .FirstOrDefault(u => (u.Email == userorEmail || u.Username == userorEmail)
                                            && u.Password == password);

            if (user == null)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                HttpContext.Session.SetString("UserType", user.oUserTypes.Name);
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetInt32("UserID", user.IdUser);

                return RedirectToAction("Index", "Home");
            }
        }




        public IActionResult Salir()
        {
            HttpContext.Session.SetString("UserType", "No registrado");
            HttpContext.Session.SetInt32("UserID", 0);
            HttpContext.Session.SetString("Username", string.Empty);
            return RedirectToAction("Index", "Home");
        }
    }
}
