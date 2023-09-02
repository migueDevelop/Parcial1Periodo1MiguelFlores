using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiParcialito.Models;

namespace MiParcialito.Controllers
{
    public class PublicoController : Controller
    {
        private readonly DboContext dboContext;

        public PublicoController(DboContext dboContext)
        {
            this.dboContext = dboContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var materiasConDocentes = await dboContext.Materias.Include(m => m.Docente)
                .ToListAsync();
            return View(materiasConDocentes);
        }
    }
}
