using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Domain.Enums;
using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;

namespace LoyaltySystem.Application.Calculators;

public class CartCalculator(DiscountStrategyResolver resolver) : ICartCalculator
{
    public CalculationResult GetCalculated(Cart cart, List<Discount> discounts,
        List<CustomerDiscount>? customerDiscounts, DateTime now)
    {
        CalculationResult result = new CalculationResult();
        List<CustomerDiscount> usedUserDiscounts = new List<CustomerDiscount>();

        foreach (var discount in discounts)
        {
            if (!DiscountIsActivatedOrAllowed(discount, customerDiscounts))
                continue;

            CustomerDiscount? customerDiscount = customerDiscounts?.Find(x => x.DiscountId == discount.Id);
            Console.WriteLine(customerDiscount.DiscountId);
            decimal? limit = customerDiscount?.ProductsLeft;
            // Console.WriteLine("apply to:" + discount.ApplyTo);
            var strategy = resolver.Get(discount.ApplyTo);
            strategy.Apply(cart, discount, (int?)limit, customerDiscount, now);
        }

        result.NewCart = cart;
        result.UsedDiscounts = usedUserDiscounts;
        return result;
    }

    private bool DiscountIsActivatedOrAllowed(Discount discount, List<CustomerDiscount>? customerDiscounts)
    {
        if (!discount.NeedActivation)
            return true;
        return customerDiscounts?.Find(x => x.DiscountId == discount.Id) != null;
    }
}