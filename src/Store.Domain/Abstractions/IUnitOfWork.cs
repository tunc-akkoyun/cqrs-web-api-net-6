using System.Threading;
using System.Threading.Tasks;

namespace Store.Domain.Abstractions;

public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}