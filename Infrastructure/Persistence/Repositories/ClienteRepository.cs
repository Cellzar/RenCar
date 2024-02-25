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

public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
{

    public ClienteRepository(PruebatecnicaContext context) : base(context)
    {
    }

    public async Task<Cliente> GetById(int id)
    {
        return await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
    }
}
