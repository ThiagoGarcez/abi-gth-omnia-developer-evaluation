using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale item in the system with authentication and profile information.
    /// This entity follows domain-driven design principles and includes business rules validation.
    /// </summary>
    public class SaleItem : BaseEntity
    {
        public Guid SaleId { get; set; }
        /// <summary>
        /// Gets or sets the name of the product
        /// </summary>
        public string Product { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of this product in this sale
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price os this product
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount of this item
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the total amount of this item
        /// </summary>
        public decimal TotalItemAmount { get; set; }

        /// <summary>
        /// Flag that sinalized if the item sale is cancel or not
        /// </summary>
        public bool Cancelled { get; set; }

        public ValidationResultDetail Validate()
        {
            var validator = new SaleItemValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }

        /// <summary>
        /// Cancel the sale item.
        /// </summary>
        public void Cancel()
        {
            Cancelled = true;
        }

        /// <summary>
        /// Uncancel the sale item.
        /// </summary>
        public void Uncancel()
        {
            Cancelled = false;
        }
    }
}
