using Microsoft.AspNetCore.Mvc;
using MiParcialito.Models;

namespace MiParcialito.Controllers
{
    public class RegistroController : Controller
    {
        private readonly DboContext _context;

        public RegistroController(DboContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(string userName, string userEmail, string userPassword, string confirmPassword, DateTime userBirthDate, int role)
        {
            if (userPassword != confirmPassword)
            {
                ViewBag.PasswordError = "Las contraseñas no coinciden";
                return View("Index");
            }

            if (role != 1 && role != 2)
            {
                ViewBag.RoleError = "Rol inválido";
                return View("Index");
            }

            var nuevoUsuario = new Usuario
            {
                UserName = userName,
                UserEmail = userEmail,
                UserPassword = userPassword,
                UserBirthDate = userBirthDate,
                RoleId = role
            };

            _context.Usuarios.Add(nuevoUsuario);
            _context.SaveChanges();

            return RedirectToAction("Index", "Access");
        }
    }
}
