using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;

namespace LoyaltySystem.Application.Abstractions;

public interface IDiscountRepo
{
    public Task<List<Discount>> GetByProductAsync(Guid productId, CancellationToken cToken);
    public Task<List<Discount>> GetByProductsAsync(List<Guid> productIds, CancellationToken cToken);
    public Task<List<UserDiscount>> GetUserDiscounts(Guid userId, CancellationToken cToken);
    public Task<Discount?> GetById(Guid id, CancellationToken cToken);
    public Task<UserDiscount?> GetUserDiscountById(Guid userId, Guid discountId, CancellationToken cToken);
    public Task AddUserDiscount(UserDiscount userDiscount, CancellationToken cToken);
    public Task UpdateUserDiscounts(Guid userId,IEnumerable<UserDiscount> userDiscounts, CancellationToken cToken);
}