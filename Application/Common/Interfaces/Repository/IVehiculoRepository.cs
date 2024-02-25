using Domain.DTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repository;

public interface IVehiculoRepository : IGenericRepository<Vehiculo>
{
    Task<IEnumerable<Vehiculo>> ObtenerVehiculosDisponiblesPorRecogida(BusquedaVehiculosDto dto);
}
