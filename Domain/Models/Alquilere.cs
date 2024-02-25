using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Alquilere : BaseEntity
{

    public int? ClienteId { get; set; }

    public int? VehiculoId { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual Vehiculo? Vehiculo { get; set; }
}
