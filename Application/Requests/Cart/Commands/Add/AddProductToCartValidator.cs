using FluentValidation;

namespace Application.Requests.Cart.Commands.Add;

public class AddProductToCartValidator : AbstractValidator<AddProductToCartCommand>
{
    public AddProductToCartValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty()
            .WithMessage("Product cannot be empty");
        RuleFor(x => x.Quantity).NotEmpty()
            .WithMessage("Quantity cannot be empty");
        
        RuleFor(x => x.Quantity).GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");
    }
}