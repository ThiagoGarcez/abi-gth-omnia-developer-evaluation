using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    /// <summary>
    /// Configures the Faker to generate valid CreateSaleCommand entities.
    /// The generated sale commands will have valid:
    /// - SaleNumber: Random long number.
    /// - SaleDate: A recent date.
    /// - TotalAmount: A monetary amount (this value may be overwritten by business rules).
    /// - Custumer: A valid customer name.
    /// - Branch: A valid branch name.
    /// - Cancelled: A boolean indicating if the sale is cancelled.
    /// - Items: A list of valid sale item commands.
    /// </summary>
    public static class CreateSaleHandlerTestData
    {
        // Faker to generate valid CreateSaleItemCommand entities.
        private static readonly Faker<CreateSaleItemCommand> saleItemFaker = new Faker<CreateSaleItemCommand>()
            .RuleFor(i => i.Product, f => f.Commerce.ProductName())
            .RuleFor(i => i.Quantity, f => f.Random.Int(1, 20))
            .RuleFor(i => i.UnitPrice, f => f.Finance.Amount(10, 100))
            .RuleFor(i => i.Discount, f => 0m) // Initial discount set to zero (will be computed by business rules)
            .RuleFor(i => i.TotalItemAmount, (f, i) => i.UnitPrice * i.Quantity)
            .RuleFor(i => i.Cancelled, f => f.Random.Bool());

        // Faker to generate valid CreateSaleCommand entities.
        private static readonly Faker<CreateSaleCommand> saleCommandFaker = new Faker<CreateSaleCommand>()
            .RuleFor(s => s.SaleNumber, f => f.Random.Long(1, 10000))
            .RuleFor(s => s.SaleDate, f => f.Date.Recent())
            .RuleFor(s => s.TotalAmount, f => f.Finance.Amount(100, 1000))
            .RuleFor(s => s.Custumer, f => f.Person.FullName)
            .RuleFor(s => s.Branch, f => f.Company.CompanyName())
            .RuleFor(s => s.Cancelled, f => f.Random.Bool())
            .RuleFor(s => s.Items, f => saleItemFaker.Generate(f.Random.Int(1, 5)).ToList());

        /// <summary>
        /// Generates a valid CreateSaleCommand with randomized data.
        /// The generated command will have all properties populated with valid values
        /// that meet the system's validation requirements.
        /// </summary>
        /// <returns>A valid CreateSaleCommand with randomly generated data.</returns>
        public static CreateSaleCommand GenerateValidCommand()
        {
            return saleCommandFaker.Generate();
        }
    }
}