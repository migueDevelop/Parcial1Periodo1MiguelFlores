using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiParcialito.Models;
using MiParcialito.Models.ViewModels;

namespace MiParcialito.Controllers
{
    public class EstudianteController : Controller
    {
        private readonly DboContext dboContext;
        int globalId;

        public EstudianteController(DboContext dboContext)
        {
            this.dboContext = dboContext;
        }

        [HttpGet]
        public async Task<IActionResult> View(int idEstudiante)
        {
            globalId = idEstudiante;
            var inscripciones = await dboContext.Inscripciones
                .Include(i => i.Materia)
                .ThenInclude(m => m.Docente)
                .Where(x => x.EstudianteId == idEstudiante)
                .ToListAsync();

            var noInscripciones = await dboContext.Inscripciones
                .Include(i => i.Materia)
                .ThenInclude(m => m.Docente)
                .Where(x => x.EstudianteId != idEstudiante)
                .ToListAsync();

            var viewModel = new InscripcionesViewModel
            {
                Inscripciones = inscripciones,
                NoInscripciones = noInscripciones
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> InscribirMateria(int idMateria)
        {
            // Obtener la materia y el estudiante (puedes obtener el estudiante de alguna manera)
            var materia = await dboContext.Materias.FindAsync(idMateria);
            var estudianteId = globalId; // Obtén el id del estudiante (por ejemplo, a través de la autenticación)

            // Crear una nueva inscripción
            var nuevaInscripcion = new Inscripcione
            {
                EstudianteId = estudianteId,
                MateriaId = idMateria
            };

            dboContext.Inscripciones.Add(nuevaInscripcion);
            await dboContext.SaveChangesAsync();

            return RedirectToAction("View", new { idEstudiante = estudianteId });
        }

    }
}
