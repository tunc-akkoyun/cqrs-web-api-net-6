using Store.Application.Abstractions.Messaging;

namespace Store.Application.Auth.Commands.Register;

public sealed record RegisterCommand(string Email, string Name, string Password) : ICommand<AuthReponse>;