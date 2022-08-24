using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Domain.Primitives;

namespace Store.Infrastructure.Extensions;

public static class IQueryableExtensions
{
    public static async Task<PagedEntity<TEntity>> ToPagedEntityListAsync<TEntity>(this IQueryable<TEntity> source, int pageIndex, int pageSize, bool getOnlyTotalCount = false)
    {
        if (source == null)
            return new PagedEntity<TEntity>(default, 0);

        //min allowed page size is 1
        pageSize = Math.Max(pageSize, 1);

        var count = await source.CountAsync();

        var data = new List<TEntity>();

        if (!getOnlyTotalCount)
            data.AddRange(await source.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync());

        return new PagedEntity<TEntity>(data, count);
    }
}