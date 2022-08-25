using System;
using Store.Domain.Abstractions;
using Store.Domain.Primitives;

namespace Store.Domain.Entities;

public sealed class User : EntityHash, IEntityDateExtended
{
    public User(string email, string name, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreatedUTC { get; set; }
    public DateTime? ModifiedUTC { get; set; }
    public DateTime? DeletedUTC { get; set; }
    public DateTime? LastLoginUTC { get; set; }
    public int PasswordErrorCount { get; set; }
}