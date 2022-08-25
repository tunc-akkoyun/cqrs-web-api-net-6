using Store.Domain.Exceptions.Base;

namespace Store.Domain.Exceptions.Users;

public sealed class UserInvalidPasswordException : BadRequestException
{
    public UserInvalidPasswordException()
        : base($"The password is invalid")
    {
    }
}

