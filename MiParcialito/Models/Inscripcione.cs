using System;
using System.Collections.Generic;

namespace MiParcialito.Models;

public partial class Inscripcione
{
    public int InscripcionId { get; set; }

    public int? EstudianteId { get; set; }

    public int? MateriaId { get; set; }

    public virtual Usuario? Estudiante { get; set; }

    public virtual Materia? Materia { get; set; }
}
