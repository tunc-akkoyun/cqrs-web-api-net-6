using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Domain.Abstractions;
using Store.Domain.Primitives;
using Store.Infrastructure.Extensions;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories;

public class EFRepository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public EFRepository(ApplicationDbContext dbContext)
    {
        if (dbContext == null)
            throw new ArgumentNullException("dbContext can not be null.");

        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }

    public IQueryable<T> GetAllQuery()
        => _dbSet;

    public IQueryable<T> GetAllQuery(Expression<Func<T, bool>> filter)
        => _dbSet.Where(filter);

    public async Task<List<T>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        => await _dbSet.Where(filter).ToListAsync();

    public async Task<PagedEntity<T>> GetAllPagedAsync(Expression<Func<T, bool>> filter, int pageIndex = 0, int pageSize = int.MaxValue, bool getTotalCountOnly = false)
        => await _dbSet.Where(filter).ToPagedEntityListAsync(pageIndex, pageSize, getTotalCountOnly);

    public async Task<T> GetAsync(int id)
        => await _dbSet.FindAsync(id);

    public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        => await _dbSet.Where(filter).SingleOrDefaultAsync();

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        => await _dbSet.AnyAsync(filter);

    public void Add(T entity)
        => _dbSet.Add(entity);

    public void Update(T entity)
    {
        if (entity.GetType().GetProperty("ModifiedUTC") != null)
            entity.GetType().GetProperty("ModifiedUTC").SetValue(entity, DateTime.UtcNow);

        this.UpdateEntityState(entity);
    }

    public void Delete(T entity)
    {
        if (entity.GetType().GetProperty("DeletedUTC") != null)
        {
            entity.GetType().GetProperty("DeletedUTC").SetValue(entity, DateTime.UtcNow);

            this.UpdateEntityState(entity);
        }
    }

    private void UpdateEntityState(T entity)
    {
        _dbSet.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
    }
}