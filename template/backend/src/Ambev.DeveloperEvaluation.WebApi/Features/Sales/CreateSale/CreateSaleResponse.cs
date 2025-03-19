namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// API response model for CreateSale operation
    /// </summary>
    public class CreateSaleResponse
    {
        /// <summary>
        /// The unique identifier of the created user
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The number of the sale
        /// </summary>
        public long SaleNumber { get; set; }
    }
}
