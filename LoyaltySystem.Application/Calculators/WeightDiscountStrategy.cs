using LoyaltySystem.Domain.Enums;
using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;

namespace LoyaltySystem.Application.Calculators;

public class WeightDiscountStrategy : IDiscountStrategy
{
    public DiscountApplyTo ApplyTo => DiscountApplyTo.Weight;

    public CustomerDiscount? Apply(Cart cart, Discount discount, int? limit, CustomerDiscount? customerDiscount, DateTime now)
    {
        var matchingItem = cart.Items
            .Find(x => (discount.ProductsId.Contains(x.ProductId) && !x.DiscountApplied));
        if (matchingItem is null)
            return null;

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
        var weightLeft = Math.Max(matchingItem.Count - (decimal)limit, 0 );
        matchingItem.DiscountApplied = true;
        matchingItem.UnitDiscount = matchingItem.UnitPrice * discount.Percent;
        if (customerDiscount is null)
            return new CustomerDiscount
                { Id = Guid.NewGuid(), DiscountId = discount.Id, ProductsLeft = weightLeft };
       
        {
            customerDiscount.LastUsedAt = now;
            customerDiscount.ProductsLeft = weightLeft;
            return null;
        }
    }
}