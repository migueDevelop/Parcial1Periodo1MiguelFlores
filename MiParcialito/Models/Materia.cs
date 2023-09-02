using System;
using System.Collections.Generic;

namespace MiParcialito.Models;

public partial class Materia
{
    public int MateriaId { get; set; }

    public string MateriaName { get; set; } = null!;

    public int? DocenteId { get; set; }

    public virtual Docente? Docente { get; set; }

    public virtual ICollection<Inscripcione> Inscripciones { get; set; } = new List<Inscripcione>();
}
