using Store.Domain.Exceptions.Base;

namespace Store.Domain.Exceptions.Users;

public sealed class UserDeletedException : BadRequestException
{
    public UserDeletedException(string email)
        : base($"The user with email {email} was deleted.")
    {
    }
}

