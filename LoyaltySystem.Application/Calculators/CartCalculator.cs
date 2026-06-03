using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Domain.Enums;
using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;

namespace LoyaltySystem.Application.Calculators;

public class CartCalculator : ICartCalculator
{
    private readonly DiscountStrategyFactory _factory;

    public CartCalculator(DiscountStrategyFactory factory)
    {
        _factory = factory;
    }

    public CalculationResult GetCalculated(Cart cart, List<Discount> discounts,
        List<UserDiscount> userDiscounts, DateTime now)
    {
        CalculationResult result = new CalculationResult( );
        List<UserDiscount> usedUserDiscounts = new List<UserDiscount>();
        
        foreach (var discount in discounts)
        {
            if (discount.NeedActivation &&
                userDiscounts.Find(x => x.DiscountId == discount.Id) is null)
                continue;

            decimal? limit = userDiscounts.Find(x => x.DiscountId == discount.Id)?.ProductsLeft;
            Console.WriteLine("apply to:" + discount.ApplyTo);
            var strategy = _factory.Get(discount.ApplyTo);
            strategy.Apply(cart, discount, (int?)limit, userDiscounts, now);
        }

        result.NewCart = cart;
        result.UsedDiscounts = usedUserDiscounts;
        return result;
    }
}