﻿using Microsoft.AspNetCore.Mvc;
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
            /*if (oUser.Password != oUser.ConfirmarPassword)
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View(oUser);
            }*/

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

            // Buscar usuario en la base de datos
            var user = _DBContext.Users
                       .Include(u => u.oUserTypes) // Asegúrate de incluir el tipo de usuario
                       .FirstOrDefault(u => (u.Email == userorEmail || u.Username == userorEmail)
                                            && u.Password == password);

            if (user == null)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                // Guardar el tipo de usuario en la sesión
                HttpContext.Session.SetString("UserType", user.oUserTypes.Name);

                return RedirectToAction("Index", "Home");
            }
        }


        public IActionResult Salir()
        {
            HttpContext.Session.SetString("UserType", "No registrado");
            return RedirectToAction("Index", "Home");
        }

        /*[HttpPost]
        public ActionResult Login(User oUser)
        {
            using (SqlConnection cn = new SqlConnection(cadenaSQL))
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("Username", oUser.IduserType);
                cmd.Parameters.AddWithValue("Email", oUser.Email);
                cmd.Parameters.AddWithValue("password_", oUser.Password);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                oUser.IdUser = Convert.ToInt32(cmd.ExecuteScalar().ToString);
            }
            if (oUser.IdUser !=0)
            {
                Session["usuario"] = oUser;
                return RedirectToAction("Index", "Home");
            } else
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
            }
            return View();
        }*/
    }
}