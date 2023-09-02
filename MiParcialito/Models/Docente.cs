using System;
using System.Collections.Generic;

namespace MiParcialito.Models;

public partial class Docente
{
    public int DocenteId { get; set; }

    public string DocenteName { get; set; } = null!;

    public virtual ICollection<Materia> Materia { get; set; } = new List<Materia>();
}
