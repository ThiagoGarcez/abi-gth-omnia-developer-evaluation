using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Validator for CreteSaleRequest that defines validation rules for user creation.
    /// </summary>
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        /// <summary>
        /// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - SaleNumber: Is required and must be greater than zero.
        /// - SaleDate: Is required and cannot be in the future.
        /// - TotalAmount: Is required and must be a non-negative value.
        /// - Custumer: Is required, cannot be null or empty, and must contain more than 2 characters.
        /// - Branch: Is required, cannot be null or empty, and must contain more than 2 characters.
        /// - Items: Must contain at least one sale item.
        /// </remarks>
        public CreateSaleRequestValidator()
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
                .SetValidator(new CreateSaleItemRequestValidator());
        }
    }

    /// <summary>
    /// Validator for the <see cref="CreateSaleItemRequest"/> class.
    /// </summary>
    public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleItemRequestValidator"/> with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Product: Is required, cannot be null or empty, and must contain more than 2 characters.
        /// - Quantity: Is required and must be greater than zero.
        /// - UnitPrice: Is required and must be greater than zero.
        /// - Discount: Must be a non-negative value.
        /// - TotalItemAmount: Must be a non-negative value.
        /// </remarks>
        public CreateSaleItemRequestValidator()
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