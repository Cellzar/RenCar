using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands;

public class CreateReservaCommand
{
    public int VehiculoId { get; set; }
    public int ClienteId { get; set; }
    public int LocalidadRecogidaId { get; set; }
    public int LocalidadDevolucionId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
}
