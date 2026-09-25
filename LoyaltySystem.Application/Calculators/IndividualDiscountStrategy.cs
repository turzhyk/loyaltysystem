using LoyaltySystem.Domain.Enums;
using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;

namespace LoyaltySystem.Application.Calculators;

public class IndividualDiscountStrategy:IDiscountStrategy
{
    public DiscountApplyTo ApplyTo => DiscountApplyTo.Individual;
    public CustomerDiscount? Apply(Cart cart, Discount discount, int? limit, CustomerDiscount? customerDiscount, DateTime now)
    {
        throw new NotImplementedException();
    }
}