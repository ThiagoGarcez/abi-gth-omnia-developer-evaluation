using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(x => x.SaleNumber)
                .GreaterThan(0)
                .WithMessage("Sale number must be greater than 0.");

            RuleFor(x => x.SaleDate)
                .NotEmpty()
                .WithMessage("Sale date is required.")
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Sale date cannot be in the future.");

            RuleFor(x => x.TotalAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total amount must be a non-negative value.");

            RuleFor(x => x.Custumer)
                .NotEmpty()
                .WithMessage("Customer name is required.")
                .MinimumLength(3)
                .WithMessage("Customer name must contain more than 2 characters.");

            RuleFor(x => x.Branch)
                .NotEmpty()
                .WithMessage("Branch name is required.")
                .MinimumLength(3)
                .WithMessage("Branch name must contain more than 2 characters.");

            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage("At least one sale item is required.");

            RuleForEach(x => x.Items)
                .SetValidator(new SaleItemValidator());
        }
    }
}
