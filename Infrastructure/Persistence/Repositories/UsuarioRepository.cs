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

public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
{
    private readonly PruebatecnicaContext _context;

    public UsuarioRepository(PruebatecnicaContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Usuario> GetById(int id)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(c => c.Id == id);
    }
}
