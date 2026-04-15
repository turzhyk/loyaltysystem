using LoyaltySystem.Domain.Enums;
using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;

namespace LoyaltySystem.Application.Calculators;

public interface IDiscountStrategy
{
    DiscountApplyTo ApplyTo { get; }
    void Apply(Cart cart, Discount discount, int? limit, List<UserDiscount> userDiscounts, DateTime now);
}