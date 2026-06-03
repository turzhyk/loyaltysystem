using LoyaltySystem.Domain.Enums;
using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;

namespace LoyaltySystem.Application.Calculators;

public class WeightDiscountStrategy : IDiscountStrategy
{
    public DiscountApplyTo ApplyTo => DiscountApplyTo.Weight;

    public void Apply(Cart cart, Discount discount, int? limit, List<UserDiscount> userDiscounts, DateTime now)
    {
        var matchingItem = cart.Items
            .Find(x => (discount.ProductsId.Contains(x.ProductId) && !x.DiscountApplied));
        if (matchingItem is null)
            return;

        limit ??= (int)discount.Limit;
        if (matchingItem.Count > limit)
        {
            cart.Items.Add(new CartItem { 
                ProductId = matchingItem.ProductId,
                UnitPrice = matchingItem.UnitPrice,
                DiscountApplied = false,
                Count = matchingItem.Count - (decimal)limit
            });
            matchingItem.Count = (decimal)limit;
        }
        matchingItem.DiscountApplied = true;
        matchingItem.UnitDiscount = matchingItem.UnitPrice * discount.Percent;
    }
}