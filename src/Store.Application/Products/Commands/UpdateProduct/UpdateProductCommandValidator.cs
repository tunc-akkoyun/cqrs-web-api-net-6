using FluentValidation;

namespace Store.Application.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Hash).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}