using LoyaltySystem.Domain.Enums;
using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;

namespace LoyaltySystem.Application.Calculators;

public class WeightDiscountStrategy:IDiscountStrategy
{
    public DiscountApplyTo ApplyTo => DiscountApplyTo.Weight;
    public void Apply(Cart cart, Discount discount, int? limit, List<UserDiscount> userDiscounts, DateTime now)
    {
        var weightedItem =
            cart.Items.Find(x => discount.ProductsId[0] == x.ProductId && !x.DiscountApplied);
        if (weightedItem == null)
            return;
        if (weightedItem.Count > discount.Limit)
        {
            var undiscountedItem = new CartItem
                { ProductId = weightedItem.ProductId, Count = weightedItem.Count - discount.Limit };
            // newCart.Items.Add(undiscountedItem);
        }

        weightedItem.UnitDiscount = weightedItem.UnitPrice * (discount.Percent / 100);
        weightedItem.DiscountApplied = true;
        // newCart.Items.Add(weightedItem);
    }
}