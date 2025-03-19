using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    /// <summary>
    /// Configures the Faker to generate valid UpdateSaleCommand entities.
    /// The generated commands will have valid:
    /// - Id: A unique identifier for the sale.
    /// - SaleNumber: A random sale number.
    /// - SaleDate: A recent date.
    /// - TotalAmount: A monetary value (this value may be recalculated by business rules).
    /// - Custumer: A valid customer name.
    /// - Branch: A valid branch name.
    /// - Cancelled: A flag indicating if the sale is cancelled.
    /// - Items: A list of valid update sale item commands.
    /// </summary>
    public static class UpdateSaleHandlerTestData
    {
        // Faker to generate valid UpdateSaleItemCommand entities.
        private static readonly Faker<UpdateSaleItemCommand> saleItemFaker = new Faker<UpdateSaleItemCommand>()
            .RuleFor(i => i.Product, f => f.Commerce.ProductName())
            .RuleFor(i => i.Quantity, f => f.Random.Int(1, 20))
            .RuleFor(i => i.UnitPrice, f => f.Finance.Amount(10, 100))
            .RuleFor(i => i.Discount, f => 0m) // Initial discount is zero (business rules will compute the final discount)
            .RuleFor(i => i.TotalItemAmount, (f, i) => i.UnitPrice * i.Quantity)
            .RuleFor(i => i.Cancelled, f => f.Random.Bool());

        // Faker to generate valid UpdateSaleCommand entities.
        private static readonly Faker<UpdateSaleCommand> saleCommandFaker = new Faker<UpdateSaleCommand>()
            .RuleFor(s => s.SaleNumber, f => f.Random.Long(1, 10000))
            .RuleFor(s => s.SaleDate, f => f.Date.Recent())
            .RuleFor(s => s.TotalAmount, f => f.Finance.Amount(100, 1000))
            .RuleFor(s => s.Custumer, f => f.Person.FullName)
            .RuleFor(s => s.Branch, f => f.Company.CompanyName())
            .RuleFor(s => s.Cancelled, f => f.Random.Bool())
            .RuleFor(s => s.Items, f => saleItemFaker.Generate(f.Random.Int(1, 5)).ToList());

        /// <summary>
        /// Generates a valid UpdateSaleCommand with randomized data.
        /// The generated command will have all properties populated with valid values
        /// that meet the system's validation requirements.
        /// </summary>
        /// <returns>A valid UpdateSaleCommand with randomly generated data.</returns>
        public static UpdateSaleCommand GenerateValidCommand()
        {
            return saleCommandFaker.Generate();
        }
    }
}
