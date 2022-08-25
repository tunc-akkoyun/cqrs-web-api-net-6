using System.Threading;
using System.Threading.Tasks;
using Store.Application.Abstractions.Messaging;
using Store.Application.Utilities;
using Store.Domain.Abstractions;
using Store.Domain.Entities;
using Store.Domain.Exceptions.Products;

namespace Store.Application.Auth.Commands.Register;

internal sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand, AuthReponse>
{
    private readonly IUnitOfWork _uow;

    public RegisterCommandHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<AuthReponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var any = await _uow.GetRepository<User>().AnyAsync(a => a.Email == request.Email);
        if (any)
        {
            throw new UserWithSameEmailException(request.Email);
        }

        var user = new User(request.Email, request.Name, request.Password);

        _uow.GetRepository<User>().Add(user);

        await _uow.SaveChangesAsync(cancellationToken);

        return new AuthReponse(user.Hash, user.Email, user.Name);
    }
}