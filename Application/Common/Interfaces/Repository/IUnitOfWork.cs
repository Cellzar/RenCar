using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repository;

public interface IUnitOfWork
{
    void SaveChanges();
    Task<int> SaveChangesAsync();
    DatabaseFacade Database { get; }
    IUsuarioRepository UsuarioRepository { get; }
    IVehiculoRepository VehiculoRepository { get; }

}
