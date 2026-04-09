using LoyaltySystem.Domain.Models.Discount;

namespace LoyaltySystem.Application.Abstractions;

public interface IDiscountRepo
{
    public Task<List<Discount>> GetByProductAsync(Guid productId, CancellationToken cToken);
}