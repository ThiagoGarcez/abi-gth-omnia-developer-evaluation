using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    /// <summary>
    /// Unit tests for the <see cref="SaleBusinessRules"/> class.
    /// </summary>
    public class SaleBusinessRulesTests
    {
        /// <summary>
        /// Tests that a sale item with a quantity greater than 20 throws an exception.
        /// </summary>
        [Fact(DisplayName = "When sale item quantity > 20 Then throws InvalidOperationException")]
        public void ValidateSaleItems_QuantityGreaterThan20_ThrowsException()
        {
            // Arrange
            var sale = new Sale
            {
                Items = new List<SaleItem>
                {
                    new SaleItem { Quantity = 21, UnitPrice = 100 }
                }
            };

            var businessRules = new SaleBusinessRules();

            // Act
            Action act = () => businessRules.ValidateSaleItems(sale);

            // Assert
            var exception = Assert.Throws<InvalidOperationException>(act);
            Assert.Equal("Cannot sell more than 20 identical items.", exception.Message);
        }

        /// <summary>
        /// Tests that sale items with quantity between 10 and 20 receive a 20% discount.
        /// </summary>
        /// <param name="quantity">The quantity of items.</param>
        /// <param name="unitPrice">The unit price of the item.</param>
        [Theory(DisplayName = "When sale item quantity is between 10 and 20 Then applies 20% discount")]
        [InlineData(10, 100)]
        [InlineData(15, 50)]
        [InlineData(20, 20)]
        public void ValidateSaleItems_QuantityBetween10And20_Applies20PercentDiscount(int quantity, decimal unitPrice)
        {
            // Arrange
            var sale = new Sale
            {
                Items = new List<SaleItem>
                {
                    new SaleItem { Quantity = quantity, UnitPrice = unitPrice }
                }
            };

            var businessRules = new SaleBusinessRules();

            // Act
            businessRules.ValidateSaleItems(sale);

            // Assert
            var expectedDiscount = unitPrice * quantity * 0.20m;
            Assert.Equal(expectedDiscount, sale.Items[0].Discount);
        }

        /// <summary>
        /// Tests that sale items with quantity between 5 and 9 receive a 10% discount.
        /// </summary>
        /// <param name="quantity">The quantity of items.</param>
        /// <param name="unitPrice">The unit price of the item.</param>
        [Theory(DisplayName = "When sale item quantity is between 5 and 9 Then applies 10% discount")]
        [InlineData(5, 100)]
        [InlineData(6, 50)]
        [InlineData(9, 20)]
        public void ValidateSaleItems_QuantityBetween5And9_Applies10PercentDiscount(int quantity, decimal unitPrice)
        {
            // Arrange
            var sale = new Sale
            {
                Items = new List<SaleItem>
                {
                    new SaleItem { Quantity = quantity, UnitPrice = unitPrice }
                }
            };

            var businessRules = new SaleBusinessRules();

            // Act
            businessRules.ValidateSaleItems(sale);

            // Assert
            var expectedDiscount = unitPrice * quantity * 0.10m;
            Assert.Equal(expectedDiscount, sale.Items[0].Discount);
        }

        /// <summary>
        /// Tests that sale items with quantity less than or equal to 4 receive no discount.
        /// </summary>
        /// <param name="quantity">The quantity of items.</param>
        /// <param name="unitPrice">The unit price of the item.</param>
        [Theory(DisplayName = "When sale item quantity is <= 4 Then no discount is applied")]
        [InlineData(1, 100)]
        [InlineData(4, 50)]
        public void ValidateSaleItems_QuantityLessThanOrEqualTo4_NoDiscount(int quantity, decimal unitPrice)
        {
            // Arrange
            var sale = new Sale
            {
                Items = new List<SaleItem>
                {
                    new SaleItem { Quantity = quantity, UnitPrice = unitPrice }
                }
            };

            var businessRules = new SaleBusinessRules();

            // Act
            businessRules.ValidateSaleItems(sale);

            // Assert
            Assert.Equal(0m, sale.Items[0].Discount);
        }
    }
}
