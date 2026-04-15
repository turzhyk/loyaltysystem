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
    public CalculationResult GetCalculated(Cart cart, List<Discount> discounts, Guid userId,
        List<UserDiscount> userDiscounts, DateTime now)
    {
        CalculationResult result = new CalculationResult { UserId = userId };
        Cart newCart = new Cart{Items = new List<CartItem>()};
        List<UserDiscount> usedUserDiscounts = new List<UserDiscount>();
        foreach (var discount in discounts)
        {
            if (discount.NeedActivation &&
                userDiscounts.Find(x => x.DiscountId == discount.Id) is null) 
                continue;
            
            decimal? limit = userDiscounts.Find(x => x.DiscountId == discount.Id)?.ProductsLeft;

            var strategy = _factory.Get(discount.ApplyTo);
            strategy.Apply(cart, discount, (int?)limit, userDiscounts, now);
        }

        result.NewCart = newCart;
        result.UsedDiscounts = usedUserDiscounts;
        throw new NotImplementedException();
    }


   
}