using Application.Common.Interfaces.Repository;
using Domain.DTO;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class VehiculoRepository : GenericRepository<Vehiculo>, IVehiculoRepository
{
    private readonly PruebatecnicaContext _context;

    public VehiculoRepository(PruebatecnicaContext context): base(context)
    {
        _context = context;
    }

    public async Task<RespuestaDto> ObtenerVehiculosDisponiblesPorLocalidades(BusquedaVehiculosDto dt)
    {
        RespuestaDto respuesta = new RespuestaDto();

        try
        {
            List<Vehiculo> vehiculosDisponibles = await _context.Vehiculos
                .Include(v => v.LocalidadRecogida)
                .Include(v => v.LocalidadDevolucion)
                .Where(v => v.LocalidadRecogida.Id == dt.LocalidadRecogidaId
                         && v.LocalidadDevolucion.Id == dt.LocalidadDevolucionId
                         && (bool)v.Disponible)
                .ToListAsync();

            if (vehiculosDisponibles == null || vehiculosDisponibles.Count == 0)
            {
                respuesta.Estado = "Error";
                respuesta.Mensaje = "No se encontraron vehículos disponibles para las localidades especificadas";
                respuesta.Ok = false;
                respuesta.Datos = null;
            }
            else
            {
                respuesta.Estado = "Éxito";
                respuesta.Mensaje = "Se encontraron vehículos disponibles correctamente";
                respuesta.Ok = true;
                respuesta.Datos = vehiculosDisponibles;
            }
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al obtener los vehículos disponibles: {ex.Message}";
            respuesta.Ok = false;
            respuesta.Datos = null;
        }

        return respuesta;
    }

    public async Task<RespuestaDto> ObtenerVehiculosDisponiblesPorMercadoYLocalidad(int mercadoId, string localidadRecogida)
    {
        RespuestaDto respuesta = new RespuestaDto();

        try
        {
            List<Vehiculo> vehiculosDisponibles = await _context.Vehiculos
                .Include(v => v.LocalidadRecogida)
                .Include(v => v.Mercado)
                .Where(v => v.MercadoId == mercadoId
                         && v.LocalidadRecogida.Nombre == localidadRecogida
                         && (bool)v.Disponible)
                .ToListAsync();

            if (vehiculosDisponibles == null || vehiculosDisponibles.Count == 0)
            {
                respuesta.Estado = "Error";
                respuesta.Mensaje = "No se encontraron vehículos disponibles para el mercado y la localidad especificados";
                respuesta.Ok = false;
                respuesta.Datos = null;
            }
            else
            {
                respuesta.Estado = "Éxito";
                respuesta.Mensaje = "Se encontraron vehículos disponibles correctamente";
                respuesta.Ok = true;
                respuesta.Datos = vehiculosDisponibles;
            }
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al obtener los vehículos disponibles: {ex.Message}";
            respuesta.Ok = false;
            respuesta.Datos = null;
        }

        return respuesta;
    }

    public async Task<RespuestaDto> ObtenerVehiculosDisponiblesPorMercadoYLocalidadDevolucion(int mercadoId, string localidadDevolucion)
    {
        RespuestaDto respuesta = new RespuestaDto();

        try
        {
            List<Vehiculo> vehiculosDisponibles = await _context.Vehiculos
                .Include(v => v.LocalidadDevolucion)
                .Include(v => v.Mercado)
                .Where(v => v.MercadoId == mercadoId
                         && v.LocalidadDevolucion.Nombre == localidadDevolucion
                         && (bool)v.Disponible)
                .ToListAsync();

            if (vehiculosDisponibles == null || vehiculosDisponibles.Count == 0)
            {
                respuesta.Estado = "Error";
                respuesta.Mensaje = "No se encontraron vehículos disponibles para el mercado y la localidad de devolución especificados";
                respuesta.Ok = false;
                respuesta.Datos = null;
            }
            else
            {
                respuesta.Estado = "Éxito";
                respuesta.Mensaje = "Se encontraron vehículos disponibles correctamente";
                respuesta.Ok = true;
                respuesta.Datos = vehiculosDisponibles;
            }
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al obtener los vehículos disponibles: {ex.Message}";
            respuesta.Ok = false;
            respuesta.Datos = null;
        }

        return respuesta;
    }
}
