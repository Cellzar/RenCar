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

    public async Task<IEnumerable<Vehiculo>> ObtenerVehiculosDisponiblesPorRecogida(BusquedaVehiculosDto dto)
    {
        var localidadRecogida = await _context.Localidades.Where(l => l.Id == dto.LocalidadRecogidaId).Select(l => l.Id).FirstOrDefaultAsync();
        var localidadDevolucion = await _context.Localidades.Where(l => l.Id == dto.LocalidadDevolucionId).Select(l => l.Id).FirstOrDefaultAsync();

        var vehiculosDisponibles = await _context.Vehiculos
            .Include(v => v.Mercado)
            .Where(v => (bool)v.Disponible && v.MercadoId == dto.MercadoId)
            .ToListAsync();

        var reservas = await _context.Reservas
            .Where(r => r.FechaRecogida <= DateTime.Now && r.FechaDevolucion >= DateTime.Now)
            .ToListAsync();

        var vehiculosReservados = reservas.Select(r => r.VehiculoId).ToList();

        return vehiculosDisponibles.Where(v => !vehiculosReservados.Contains(v.Id)).ToList();
    }
}
