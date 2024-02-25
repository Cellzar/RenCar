using System;
using System.Collections.Generic;

namespace Domain.Models;
public partial class Mercado : BaseEntity
{
    public string Nombre { get; set; } = null!;

    public virtual ICollection<Vehiculo> Vehiculos { get; } = new List<Vehiculo>();
}
