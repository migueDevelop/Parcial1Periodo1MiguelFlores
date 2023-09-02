using System;
using System.Collections.Generic;

namespace MiParcialito.Models;

public partial class Usuario
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public DateTime? UserBirthDate { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<Inscripcione> Inscripciones { get; set; } = new List<Inscripcione>();

    public virtual Role? Role { get; set; }
}
