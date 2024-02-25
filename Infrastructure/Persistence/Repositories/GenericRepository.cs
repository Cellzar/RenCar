using Application.Common.Exceptions;
using Application.Common.Interfaces.Repository;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected DbSet<T> _entities;

    public GenericRepository(DbContext dbContext)
    {
        this._entities = dbContext.Set<T>();
    }

    public async Task<List<T>> GetAll()
    {

        return await _entities.ToListAsync();
    }

    public async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate)
    {

        return await _entities.Where(predicate).ToListAsync();
    }

    public async Task<T?> GetById(int id) => await _entities.FindAsync(id);

    public async Task<T?> Get(Expression<Func<T, bool>> predicate)
    {

        return await _entities.Where(predicate).FirstOrDefaultAsync();
    }

    public async Task Add(T entity)
    {

        await _entities.AddAsync(entity);
    }

    public async Task Delete(int id)
    {

        T? entity = await GetById(id);
        if (entity != null)
        {
            this._entities.Remove(entity);
        }
    }

    public void Update(T entity)
    {

        try
        {
            _entities.Update(entity);
        }
        catch (Exception ex)
        {
            throw new GeneralException($"ExMessage: {ex.Message}");
        }
    }
    public async Task<int> CountRecord()
    {

        return await _entities.CountAsync();
    }
}
