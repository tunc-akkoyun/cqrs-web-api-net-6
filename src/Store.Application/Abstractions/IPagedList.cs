using System;
using System.Collections.Generic;

namespace Store.Application.Abstractions;

public interface IPagedList<T>
{
    int PageIndex { get; }
    int PageSize { get; }
    int TotalCount { get; }
    int TotalPages { get; }
    List<T> Data { get; }
}

public sealed record PagedList<T> : IPagedList<T>
{
    public int PageIndex { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public int TotalPages { get; }
    public bool HasPreviousPage => PageIndex > 0;
    public bool HasNextPage => PageIndex + 1 < TotalPages;
    public List<T> Data { get; }

    public PagedList(List<T> source, int totalCount, int pageIndex, int pageSize)
    {
        //min allowed page size is 1
        pageSize = Math.Max(pageSize, 1);

        TotalCount = totalCount;
        TotalPages = TotalCount / pageSize;

        if (TotalCount % pageSize > 0)
            TotalPages++;

        PageSize = pageSize;
        PageIndex = pageIndex;

        Data = source;
    }
}