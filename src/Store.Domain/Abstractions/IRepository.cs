using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Store.Domain.Primitives;

namespace Store.Domain.Abstractions;

public interface IRepository<T> where T : class
{
    IQueryable<T> GetAllQuery();
    IQueryable<T> GetAllQuery(Expression<Func<T, bool>> filter);

    Task<List<T>> GetAllAsync();
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter);

    Task<PagedEntity<T>> GetAllPaged(Expression<Func<T, bool>> filter, int pageIndex = 0, int pageSize = int.MaxValue, bool getTotalCountOnly = false);

    Task<T> GetAsync(int id);
    Task<T> GetAsync(Expression<Func<T, bool>> filter);

    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}