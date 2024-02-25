using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Localidade : BaseEntity
{

    public string? Nombre { get; set; }

    public virtual ICollection<Mercado> Mercados { get; } = new List<Mercado>();

    public virtual ICollection<Vehiculo> Vehiculos { get; } = new List<Vehiculo>();
}
