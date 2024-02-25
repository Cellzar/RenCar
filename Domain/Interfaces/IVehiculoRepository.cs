using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IVehiculoRepository : IGenericRepository<Vehiculo>
    {
        Task<IEnumerable<Vehiculo>> GetDisponibles(int localidadRecogidaId, int localidadDevolucionId, int mercadoId);
    }
}
