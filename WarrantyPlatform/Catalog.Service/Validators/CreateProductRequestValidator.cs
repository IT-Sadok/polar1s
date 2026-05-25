using Catalog.Service.Models;
using FluentValidation;

namespace Catalog.Service.Validators;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .MaximumLength(100)
            .WithMessage("Product name must not exceed 100 characters.");

        RuleFor(x => x.Sku)
            .NotEmpty()
            .WithMessage("SKU is required.")
            .MaximumLength(50)
            .WithMessage("SKU must not exceed 50 characters.");

        RuleFor(x => x.Category)
            .IsInEnum()
            .WithMessage("Invalid product category.");

        RuleFor(x => x.BrandId)
            .NotEmpty()
            .WithMessage("Brand ID is required.");
    }
}
