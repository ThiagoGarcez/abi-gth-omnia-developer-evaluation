namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// API response model for UpdateSale operation
    /// </summary>
    public class UpdateSaleResponse
    {
        /// <summary>
        /// The unique identifier of the update user
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The number of the sale
        /// </summary>
        public long SaleNumber { get; set; }
    }
}
