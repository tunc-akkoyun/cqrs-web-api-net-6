using Store.Domain.Exceptions.Base;

namespace Store.Domain.Exceptions.Users;

public sealed class UserIsLockedException : BadRequestException
{
    public UserIsLockedException(string email)
        : base($"The user with email {email} was locked.")
    {
    }
}

