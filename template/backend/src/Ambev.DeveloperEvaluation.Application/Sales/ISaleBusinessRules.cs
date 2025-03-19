using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public interface ISaleBusinessRules
    {
        void ValidateSaleItems(Sale sale);
    }
}