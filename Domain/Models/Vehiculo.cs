using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Vehiculo : BaseEntity
{

    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public int? LocalidadId { get; set; }

    public bool? Disponible { get; set; }

    public virtual ICollection<Alquilere> Alquileres { get; } = new List<Alquilere>();

    public virtual Localidade? Localidad { get; set; }
}
