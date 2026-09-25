using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;

namespace LoyaltySystem.Application.Abstractions;

public interface IDiscountRepo
{
    public Task<List<Discount>> GetByProductAsync(Guid productId, CancellationToken cToken);
    public Task<List<Discount>> GetByProductsAsync(IEnumerable<Guid> productIds, CancellationToken cToken);
    public Task<List<CustomerDiscount>> GetUserDiscountsByUserIdAsync(Guid userId, CancellationToken cToken);
    public Task<Discount?> GetById(Guid id, CancellationToken cToken);
    public Task<CustomerDiscount?> GetUserDiscountById(Guid userId, Guid discountId, CancellationToken cToken);
    public Task AddUserDiscount(CustomerDiscount customerDiscount, CancellationToken cToken);
    public Task UpdateUserDiscounts(Guid userId,IEnumerable<CustomerDiscount> userDiscounts, CancellationToken cToken);
}