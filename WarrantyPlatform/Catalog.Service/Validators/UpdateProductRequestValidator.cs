using Catalog.Service.Models;
using FluentValidation;

namespace Catalog.Service.Validators;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .MaximumLength(100)
            .WithMessage("Product name must not exceed 100 characters.");

        RuleFor(x => x.Category)
            .IsInEnum()
            .WithMessage("Invalid product category.");
    }
}
