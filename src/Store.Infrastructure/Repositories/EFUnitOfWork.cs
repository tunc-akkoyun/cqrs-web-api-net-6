using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Domain.Abstractions;

namespace Store.Infrastructure.Repositories;

public class EFUnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public EFUnitOfWork(ApplicationDbContext dbContext)
    {
        //Database.SetInitializer<ApplicationDbContext>(null);

        if (dbContext == null)
            throw new ArgumentNullException("dbContext can not be null.");

        _dbContext = dbContext;

        //_dbContext.Configuration.LazyLoadingEnabled = false;
        //_dbContext.Configuration.ValidateOnSaveEnabled = false;
        //_dbContext.Configuration.ProxyCreationEnabled = false;
    }

    public IRepository<T> GetRepository<T>() where T : class => new EFRepository<T>(_dbContext);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _dbContext.SaveChangesAsync();
}