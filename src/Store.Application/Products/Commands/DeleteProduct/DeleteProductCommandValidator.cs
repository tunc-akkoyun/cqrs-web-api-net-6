using FluentValidation;

namespace Store.Application.Products.Commands.DeleteProduct;

public sealed class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Hash).NotEmpty();
    }
}