using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Handler for processing UpdateSaleCommand requests
    /// </summary>
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ISaleBusinessRules _saleBusinessRules;

        /// <summary>
        /// Initializes a new instance of UpdateSaleHandler
        /// </summary>
        /// <param name="saleRepository">The sale repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public UpdateSaleHandler(ISaleRepository saleRepository,
            IMapper mapper,
            ISaleBusinessRules saleBusinessRules)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _saleBusinessRules = saleBusinessRules;
        }

        /// <summary>
        /// Handles the UpdateSaleCommand request
        /// </summary>
        /// <param name="command">The UpdateSale command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The update sale details</returns>
        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
            if (sale == null)
                throw new InvalidOperationException($"Sale not found!");

            if (sale == null)
                throw new Exception("Venda não encontrada.");

            sale.SaleNumber = command.SaleNumber;
            sale.SaleDate = command.SaleDate;
            sale.TotalAmount = command.TotalAmount;
            sale.Custumer = command.Custumer;
            sale.Branch = command.Branch;
            sale.Cancelled = command.Cancelled;

            sale.Items.Clear();
            foreach (var newItem in command.Items)
            {
                sale.Items.Add(new SaleItem
                {
                    Product = newItem.Product,
                    Quantity = newItem.Quantity,
                    UnitPrice = newItem.UnitPrice,
                    Discount = newItem.Discount,
                    TotalItemAmount = newItem.TotalItemAmount,
                    Cancelled = newItem.Cancelled
                });
            }

            _saleBusinessRules.ValidateSaleItems(sale);

            var resultUpdate = await _saleRepository.UpdateAsync(sale, cancellationToken);
            var result = _mapper.Map<UpdateSaleResult>(resultUpdate);
            return result;
        }
    }
}
