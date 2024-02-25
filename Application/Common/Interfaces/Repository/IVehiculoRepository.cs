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
    Task<RespuestaDto> ObtenerVehiculosDisponiblesPorLocalidades(BusquedaVehiculosDto dt);
    Task<RespuestaDto> ObtenerVehiculosDisponiblesPorMercadoYLocalidad(int mercadoId, string localidadRecogida);
    Task<RespuestaDto> ObtenerVehiculosDisponiblesPorMercadoYLocalidadDevolucion(int mercadoId, string localidadDevolucion);
}
