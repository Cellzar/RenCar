using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Vehiculo : BaseEntity
{

    public string Marca { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public int Año { get; set; }

    public bool? Disponible { get; set; }

    public int LocalidadRecogidaId { get; set; }

    public int LocalidadDevolucionId { get; set; }

    public int MercadoId { get; set; }

    public virtual Localidade LocalidadDevolucion { get; set; } = null!;

    public virtual Localidade LocalidadRecogida { get; set; } = null!;

    public virtual Mercado Mercado { get; set; } = null!;
}
