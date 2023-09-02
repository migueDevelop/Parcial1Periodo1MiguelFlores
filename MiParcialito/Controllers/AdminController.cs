using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiParcialito.Models;

namespace MiParcialito.Controllers
{
    public class AdminController : Controller
    {
        private readonly DboContext _context;

        public AdminController(DboContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var materias = _context.Materias.Include(m => m.Docente).ToList();
            return View(materias);
        }

        [HttpPost]
        public IActionResult AddMateria(string materiaName, string docenteName)
        {
            if (!string.IsNullOrEmpty(materiaName))
            {
                Materia nuevaMateria = new Materia { MateriaName = materiaName };

                if (!string.IsNullOrEmpty(docenteName))
                {
                    var docente = _context.Docentes.FirstOrDefault(d => d.DocenteName == docenteName);
                    if (docente != null)
                    {
                        nuevaMateria.Docente = docente;
                    }
                    else
                    {
                        ViewBag.DocenteError = "El docente no existe";
                        var materias = _context.Materias.Include(m => m.Docente).ToList();
                        return View("Index", materias);
                    }
                }

                _context.Materias.Add(nuevaMateria);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddDocente(string docenteName)
        {
            if (!string.IsNullOrEmpty(docenteName))
            {
                _context.Docentes.Add(new Docente { DocenteName = docenteName });
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
