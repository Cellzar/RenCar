using Application.Common.Interfaces.Repository;
using Infrastructure.Context;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly PruebatecnicaContext _context;

    public UnitOfWork(PruebatecnicaContext context)
    {
        _context = context;
    }

    public IUsuarioRepository UsuarioRepository => new UsuarioRepository(_context);

    public IVehiculoRepository VehiculoRepository => new VehiculoRepository(_context);

    public ILocalidadRepository LocalidadRepository => new LocalidadRepository(_context);

    public IMercadoRepository MercadoRepository => new MercadoRepository(_context);


    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public DatabaseFacade Database
    {
        get { return _context.Database; }
    }
}
