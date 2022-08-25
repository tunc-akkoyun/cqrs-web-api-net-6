using System;

namespace Store.Application.Auth;

public sealed record AuthReponse
{
    public Guid Hash { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Token { get; set; }

    public AuthReponse(Guid hash, string email, string name)
    {
        Hash = hash;
        Email = email;
        Name = name;
    }
}