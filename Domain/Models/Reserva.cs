using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Reserva : BaseEntity
{
    public int? UsuarioId { get; set; }

    public DateTime FechaRecogida { get; set; }

    public DateTime FechaDevolucion { get; set; }

    public int VehiculoId { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
