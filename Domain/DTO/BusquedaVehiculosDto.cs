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
}
