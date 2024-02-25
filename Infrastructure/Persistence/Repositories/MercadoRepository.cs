using Application.Common.Interfaces.Repository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories;

public class MercadoRepository : GenericRepository<Mercado>, IMercadoRepository
{
    private readonly DbContext _dbContext;

    public MercadoRepository(DbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
