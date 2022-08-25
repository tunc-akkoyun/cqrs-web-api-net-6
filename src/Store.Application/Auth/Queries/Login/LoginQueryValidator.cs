using FluentValidation;

namespace Store.Application.Auth.Queries.Login;

public sealed class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(250).MinimumLength(6);
    }
}