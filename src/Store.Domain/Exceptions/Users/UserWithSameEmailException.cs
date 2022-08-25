using Store.Domain.Exceptions.Base;

namespace Store.Domain.Exceptions.Products;

public sealed class UserWithSameEmailException : BadRequestException
{
    public UserWithSameEmailException(string email)
        : base($"User with '{email}' email adress already exists.")
    {
    }
}

