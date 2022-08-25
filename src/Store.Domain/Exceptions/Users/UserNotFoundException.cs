using Store.Domain.Exceptions.Base;

namespace Store.Domain.Exceptions.Users;

public sealed class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string email)
        : base($"The user with email {email} was not found.")
    {
    }
}

