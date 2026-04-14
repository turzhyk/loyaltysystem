using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;

namespace LoyaltySystem.Application.Abstractions;

public interface IDiscountRepo
{
    public Task<List<Discount>> GetByProductAsync(Guid productId, CancellationToken cToken);
    public Task<List<UserDiscount>> GetUserDiscounts(Guid userId, CancellationToken cToken);
}