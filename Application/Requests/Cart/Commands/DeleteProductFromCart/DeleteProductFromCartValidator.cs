using FluentValidation;

namespace Application.Requests.Cart.Commands.DeleteProduct;

public class DeleteProductFromCartValidator : AbstractValidator<DeleteProductFromCartCommand>
{
    public DeleteProductFromCartValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product ID cannot be empty");
        RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("Product ID must be greater than 0");
    }
}