using Application.Common.Interfaces.Repository;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories;

public class VehiculoRepository : GenericRepository<Vehiculo>, IVehiculoRepository
{
    private readonly PruebatecnicaContext _context;

    public VehiculoRepository(PruebatecnicaContext context): base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Vehiculo>> ObtenerVehiculosDisponiblesPorRecogida(int localidadRecogidaId, int localidadDevolucionId, int mercadoId)
    {
        var localidadRecogida = await _context.Localidades.Where(l => l.Id == localidadRecogidaId).Select(l => l.Id).FirstOrDefaultAsync();
        var localidadDevolucion = await _context.Localidades.Where(l => l.Id == localidadDevolucionId).Select(l => l.Id).FirstOrDefaultAsync();

        var vehiculosDisponibles = await _context.Vehiculos
            .Include(v => v.Mercado)
            .Where(v => (bool)v.Disponible && v.MercadoId == mercadoId)
            .ToListAsync();

        var reservas = await _context.Reservas
            .Where(r => r.FechaRecogida <= DateTime.Now && r.FechaDevolucion >= DateTime.Now)
            .ToListAsync();

        var vehiculosReservados = reservas.Select(r => r.VehiculoId).ToList();

        return vehiculosDisponibles.Where(v => !vehiculosReservados.Contains(v.Id)).ToList();
    }
}
