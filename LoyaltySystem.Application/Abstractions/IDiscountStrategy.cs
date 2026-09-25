using LoyaltySystem.Domain.Enums;
using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;

namespace LoyaltySystem.Application.Calculators;

public interface IDiscountStrategy
{
    DiscountApplyTo ApplyTo { get; }
    CustomerDiscount? Apply(Cart cart, Discount discount, int? limit, CustomerDiscount? customerDiscount, DateTime now);
}