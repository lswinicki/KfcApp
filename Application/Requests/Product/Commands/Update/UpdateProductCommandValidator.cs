using FluentValidation;

namespace Application.Requests.Product.Commands.Update;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero");
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty");
        
        RuleFor(x => x.Description)
            .NotEmpty().Length(1,255)
            .WithMessage("Description has to be between 1 and 255 characters");
    }
}