using Store.Application.Utilities;

namespace Store.Application.Auth.Queries.Login;

public sealed record LoginRequest
{
    public string Email { get; private set; }
    public string Password { get; private  set; }

    public LoginRequest(string email, string password)
    {
        Email = email;
        Password = PasswordGenerator.Create(password);
    }
}