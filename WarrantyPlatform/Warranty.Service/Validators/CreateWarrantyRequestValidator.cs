using FluentValidation;
using Warranty.Service.Models;

namespace Warranty.Service.Validators;

public class CreateWarrantyRequestValidator : AbstractValidator<CreateWarrantyRequest>
{
    public CreateWarrantyRequestValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.PurchaseDate).LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("PurchaseDate cannot be in the future");
        RuleFor(x => x.ExpiresAt).GreaterThan(x => x.PurchaseDate)
            .WithMessage("ExpiresAt must be after PurchaseDate");
    }
}
