using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Vehiculo : BaseEntity
{
    public string Marca { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public int Year { get; set; }

    public bool? Disponible { get; set; }

    public int? MercadoId { get; set; }

    public virtual Mercado? Mercado { get; set; }
}
