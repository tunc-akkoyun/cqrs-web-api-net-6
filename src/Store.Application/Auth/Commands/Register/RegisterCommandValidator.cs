using FluentValidation;

namespace Store.Application.Auth.Commands.Register;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(250).MinimumLength(6);
    }
}