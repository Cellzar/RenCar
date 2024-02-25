using Domain.Interfaces;
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

    public VehiculoRepository(PruebatecnicaContext context): base(context) { }

    public async Task<IEnumerable<Vehiculo>> GetDisponibles(int localidadRecogidaId, int localidadDevolucionId, int mercadoId)
    {
        var vehiculosDisponibles = await _context.Vehiculos
        .Include(v => v.Localidad)
        .Where(v => (bool)v.Disponible && v.LocalidadId == localidadRecogidaId && v.Localidad.Mercados.Any(m => m.Id == mercadoId))
        .ToListAsync();

        vehiculosDisponibles = vehiculosDisponibles
            .Where(v => v.LocalidadId == localidadDevolucionId)
            .ToList();

        return vehiculosDisponibles;
    }
}
