using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiParcialito.Models;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace MiParcialito.Controllers
{
    public class AccessController : Controller
    {
        private readonly DboContext dboContext;

        public AccessController(DboContext dboContext)
        {
            this.dboContext = dboContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Usuario usuario)
        {
            var user = dboContext.Usuarios.FirstOrDefault(u => u.UserEmail == usuario.UserEmail);

            if (user != null && (user.UserPassword == usuario.UserPassword || BCrypt.Net.BCrypt.Verify(usuario.UserPassword, user.UserPassword)))
            {
                if (user.RoleId == 1)
                {
                    return RedirectToAction("Index", "Publico");
                }
                else if (user.RoleId == 2)
                {
                    return RedirectToAction("View", "Estudiante", new { idEstudiante = user.UserId });
                }
                else if (user.RoleId == 3) {
                    return RedirectToAction("Index", "Admin");
                }

                return RedirectToAction("Error", "Home");
            }
            else {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult Salir()
        {
             return RedirectToAction("Index", "Access");
        }
    }
}
