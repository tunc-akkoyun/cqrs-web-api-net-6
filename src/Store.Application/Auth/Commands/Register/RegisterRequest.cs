using Store.Application.Utilities;

namespace Store.Application.Auth.Queries.Login;

public sealed record RegisterRequest
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    public RegisterRequest(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = PasswordGenerator.Create(password);
    }
}