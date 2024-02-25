using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Mercado : BaseEntity
{

    public string? Nombre { get; set; }

    public int? LocalidadId { get; set; }

    public virtual Localidade? Localidad { get; set; }
}
