using Store.Application.Abstractions.Messaging;

namespace Store.Application.Auth.Queries.Login;

public sealed record LoginQuery(string Email, string Password) : IQuery<AuthReponse>;