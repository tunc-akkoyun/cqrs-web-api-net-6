using System.Threading;
using System.Threading.Tasks;
using Store.Application.Abstractions.Messaging;
using Store.Domain.Abstractions;
using Store.Domain.Entities;
using Store.Domain.Exceptions.Users;

namespace Store.Application.Auth.Queries.Login;

internal sealed class LoginQueryHandler : IQueryHandler<LoginQuery, AuthReponse>
{
    private readonly IUnitOfWork _uow;

    public LoginQueryHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<AuthReponse> Handle(
        LoginQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _uow.GetRepository<User>().GetAsync(w => w.Email == request.Email);
        if (user == null)
        {
            throw new UserNotFoundException(request.Email);
        }

        if (user.DeletedUTC.HasValue)
        {
            throw new UserDeletedException(request.Email);
        }

        if (user.PasswordErrorCount >= 3)
        {
            throw new UserIsLockedException(request.Email);
        }

        if (user.Password != request.Password)
        {
            user.PasswordErrorCount += 1;

            _uow.GetRepository<User>().Update(user);
            await _uow.SaveChangesAsync();

            throw new UserInvalidPasswordException();
        }

        return new AuthReponse(user.Hash, user.Email, user.Name);
    }
}