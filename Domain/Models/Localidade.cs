using System.Collections.Generic;

namespace Domain.Models;

public partial class Localidade : BaseEntity
{

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Vehiculo> VehiculoLocalidadDevolucions { get; } = new List<Vehiculo>();

    public virtual ICollection<Vehiculo> VehiculoLocalidadRecogida { get; } = new List<Vehiculo>();
}
