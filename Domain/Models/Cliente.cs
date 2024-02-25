using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Cliente : BaseEntity
{

    public string? Nombre { get; set; }

    public string? Email { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Alquilere> Alquileres { get; } = new List<Alquilere>();
}
