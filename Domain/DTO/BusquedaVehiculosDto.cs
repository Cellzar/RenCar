using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO;

public class BusquedaVehiculosDto
{
    public int? LocalidadRecogidaId { get; set; }
    public int? LocalidadDevolucionId { get; set; }
    public int? MercadoId { get; set; }
    public DateTime FechaDevolucion { get; set; } = DateTime.Now;
    public DateTime FechaRecogida { get; set; } = DateTime.Now;
}
