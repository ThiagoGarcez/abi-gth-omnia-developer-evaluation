using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    /// <summary>
    /// Unit tests for the <see cref="UpdateSaleHandler"/> class.
    /// </summary>
    public class UpdateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ISaleBusinessRules _saleBusinessRules;
        private readonly UpdateSaleHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleHandlerTests"/> class.
        /// Sets up test dependencies and creates the handler instance.
        /// </summary>
        public UpdateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _saleBusinessRules = Substitute.For<ISaleBusinessRules>();
            _handler = new UpdateSaleHandler(_saleRepository, _mapper, _saleBusinessRules);
        }

        /// <summary>
        /// Tests that a valid sale update request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given valid sale data When updating sale Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = UpdateSaleHandlerTestData.GenerateValidCommand();

            var sale = new Sale
            {
                SaleNumber = command.SaleNumber,
                SaleDate = command.SaleDate,
                TotalAmount = command.TotalAmount,
                Custumer = command.Custumer,
                Branch = command.Branch,
                Cancelled = command.Cancelled,
                Items = command.Items.ConvertAll(i => new SaleItem
                {
                    Product = i.Product,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Discount = i.Discount,
                    TotalItemAmount = i.TotalItemAmount,
                    Cancelled = i.Cancelled
                })
            };

            var result = new UpdateSaleResult
            {
                Id = sale.Id,
            };

            _mapper.Map<Sale>(command).Returns(sale);
            _mapper.Map<UpdateSaleResult>(sale).Returns(result);
            _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns(sale);

            // When
            var updateSaleResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            updateSaleResult.Should().NotBeNull();
            updateSaleResult.Id.Should().Be(sale.Id);
            await _saleRepository.Received(1).UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
            _saleBusinessRules.Received(1).ValidateSaleItems(sale);
        }

        /// <summary>
        /// Tests that an invalid sale update request throws a validation exception.
        /// </summary>
        [Fact(DisplayName = "Given invalid sale data When updating sale Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Given an empty command that fails validation.
            var command = new UpdateSaleCommand();

            // When
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }

        /// <summary>
        /// Tests that the mapper is called with the correct command to map it to a sale entity.
        /// </summary>
        [Fact(DisplayName = "Given valid command When updating sale Then maps command to sale entity")]
        public async Task Handle_ValidRequest_MapsCommandToSale()
        {
            // Given
            var command = UpdateSaleHandlerTestData.GenerateValidCommand();

            var sale = new Sale
            {
                SaleNumber = command.SaleNumber,
                SaleDate = command.SaleDate,
                TotalAmount = command.TotalAmount,
                Custumer = command.Custumer,
                Branch = command.Branch,
                Cancelled = command.Cancelled,
                Items = new List<SaleItem>()
            };

            _mapper.Map<Sale>(command).Returns(sale);
            _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns(sale);
            var result = new UpdateSaleResult { Id = sale.Id };
            _mapper.Map<UpdateSaleResult>(sale).Returns(result);

            // When
            await _handler.Handle(command, CancellationToken.None);

            // Then
            _mapper.Received(1).Map<Sale>(Arg.Is<UpdateSaleCommand>(c =>
                c.SaleNumber == command.SaleNumber &&
                c.SaleDate == command.SaleDate &&
                c.TotalAmount == command.TotalAmount &&
                c.Custumer == command.Custumer &&
                c.Branch == command.Branch &&
                c.Cancelled == command.Cancelled));
        }
    }
}
