namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Represents a request to create a new sale record in the system.
    /// </summary>
    public class CreateSaleRequest
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
        public List<CreateSaleItemRequest> Items { get; set; } = new List<CreateSaleItemRequest>();

        /// <summary>
        /// Flag that sinalized if the sale is cancel or not
        /// </summary>
        public bool Cancelled { get; set; }
    }

    public class CreateSaleItemRequest
    {
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

    }
}
