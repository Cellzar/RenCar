using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Common.Interfaces.Repository;

public interface IUnitOfWork
{
    void SaveChanges();
    Task<int> SaveChangesAsync();
    DatabaseFacade Database { get; }
    IUsuarioRepository UsuarioRepository { get; }
    IVehiculoRepository VehiculoRepository { get; }
    ILocalidadRepository LocalidadRepository { get; }
    IMercadoRepository MercadoRepository { get; }


}
