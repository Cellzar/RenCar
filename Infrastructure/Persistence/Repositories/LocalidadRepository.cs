using Application.Common.Interfaces.Repository;
using Domain.Models;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories;

public class LocalidadRepository : GenericRepository<Localidade>, ILocalidadRepository
{
    private readonly PruebatecnicaContext _context;
    public LocalidadRepository(PruebatecnicaContext context) : base(context)
    {
        _context = context;
    }
}
