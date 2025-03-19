using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleItemValidator : AbstractValidator<SaleItem>
    {
        public SaleItemValidator()
        {
            RuleFor(x => x.Product)
                .NotEmpty()
                .WithMessage("Product name is required.")
                .MinimumLength(3)
                .WithMessage("Product name must contain more than 2 characters.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0)
                .WithMessage("Unit price must be greater than 0.");

            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Discount must be a non-negative value.");

            RuleFor(x => x.TotalItemAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total item amount must be a non-negative value.");
        }
    }
}
