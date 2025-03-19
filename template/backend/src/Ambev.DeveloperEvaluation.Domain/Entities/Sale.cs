using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale in the system with authentication and profile information.
    /// This entity follows domain-driven design principles and includes business rules validation.
    /// </summary>
    public class Sale : BaseEntity
    {
        /// <summary>
        /// Gets or sets the sale number. Must be unique.
        /// </summary>
        public long SaleNumber { get; set; }

        /// <summary>
        /// Gets or sets the date of the sale.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets or sets the date of the sale.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the name of the custumer
        /// </summary>
        public string Custumer { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the Branch
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// A List of items of the sale
        /// </summary>
        public List<SaleItem> Items { get; set; } = new List<SaleItem>();

        /// <summary>
        /// Flag that sinalized if the sale is cancel or not
        /// </summary>
        public bool Cancelled { get; set; }

        /// <summary>
        /// Performs validation of the sale entity using the SaleValidator rules.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - IsValid: Indicates whether all validation rules passed
        /// - Errors: Collection of validation errors if any rules failed
        /// </returns>
        /// Validation rules include:
        /// - SaleNumber: Is required and must be greater than zero.
        /// - SaleDate: Is required and cannot be in the future.
        /// - TotalAmount: Is required and must be a non-negative value.
        /// - Custumer: Is required, cannot be null or empty, and must contain more than 2 characters.
        /// - Branch: Is required, cannot be null or empty, and must contain more than 2 characters.
        /// - Items: Must contain at least one sale item.
        /// </remarks>
        public ValidationResultDetail Validate()
        {
            var validator = new SaleValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }

        /// <summary>
        /// Cancel the sale.
        /// </summary>
        public void Cancel()
        {
            Cancelled = true;
        }

        /// <summary>
        /// Uncancel the sale.
        /// </summary>
        public void Uncancel()
        {
            Cancelled = false;
        }
    }
}
