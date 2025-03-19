using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    /// <summary>
    /// Provides business rule validations for sale operations.
    /// </summary>
    public class SaleBusinessRules : ISaleBusinessRules
    {
        /// <summary>
        /// Validates the sale items based on business rules.
        /// </summary>
        /// <remarks>
        /// Business rules include:
        /// - Purchases below 4 items receive no discount.
        /// - Purchases above 4 identical items (i.e., quantities between 5 and 9) receive a 10% discount.
        /// - Purchases between 10 and 20 identical items receive a 20% discount.
        /// - It is not possible to sell more than 20 identical items.
        /// </remarks>
        /// <param name="sale">The sale entity to validate.</param>
        /// <exception cref="InvalidOperationException">Thrown when a sale item violates business rules.</exception>
        public void ValidateSaleItems(Sale sale)
        {
            foreach (var item in sale.Items)
            {
                if (item.Quantity > 20)
                {
                    throw new InvalidOperationException("Cannot sell more than 20 identical items.");
                }

                if (item.Quantity >= 10 && item.Quantity <= 20)
                {
                    // Apply a 20% discount.
                    item.Discount = item.UnitPrice * item.Quantity * 0.20m;
                }
                else if (item.Quantity > 4 && item.Quantity < 10)
                {
                    // Apply a 10% discount.
                    item.Discount = item.UnitPrice * item.Quantity * 0.10m;
                }
                else
                {
                    // Purchases below or equal to 4 items receive no discount.
                    item.Discount = 0;
                }
            }
        }
    }
}
