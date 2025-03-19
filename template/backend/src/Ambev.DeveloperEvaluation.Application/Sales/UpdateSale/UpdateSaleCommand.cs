using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Command for updating a sale.
    /// </summary>
    /// <remarks>
    /// This command is used to capture the required data for updating a sale, 
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
    /// that returns a <see cref="UpdateSaleResult"/>.
    /// 
    /// The data provided in this command is validated using the 
    /// <see cref="UpdateSaleCommandValidator"/> which extends 
    /// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
    /// populated and follow the required rules.
    /// </remarks>
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        public Guid Id { get; set; }
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
        public List<UpdateSaleItemCommand> Items { get; set; } = new List<UpdateSaleItemCommand>();

        /// <summary>
        /// Flag that sinalized if the sale is cancel or not
        /// </summary>
        public bool Cancelled { get; set; }

        public ValidationResultDetail Validate()
        {
            var validator = new UpdateSaleCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }

    public class UpdateSaleItemCommand
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