using LoyaltySystem.Domain.Enums;
using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;

namespace LoyaltySystem.Application.Calculators;

public class CheapestOneStrategy : IDiscountStrategy
{
    public DiscountApplyTo ApplyTo => DiscountApplyTo.Cheapest;
    public void Apply(Cart cart, Discount discount, int? limit, List<UserDiscount> userDiscounts, DateTime now)
    {
        var matchingItems = cart.Items
            .FindAll(x => (discount.ProductsId.Contains(x.ProductId) && !x.DiscountApplied));
        Console.WriteLine("matching items: "+ matchingItems.Count);
        if(matchingItems.Count == 0)
            return;
        limit ??= (int)discount.Limit;
        int maxByCount = matchingItems.Count - matchingItems.Count % discount.GroupSize;
        int discountableItemsCount = Math.Min(maxByCount, limit??1000);
        if (discountableItemsCount == 0)
            return;
        
        var group = matchingItems
            .OrderBy(x => x.UnitPrice)
            .Take(discountableItemsCount).OrderBy(i=>i.UnitPrice);
        var discountableItem = group.FirstOrDefault();
        if(group.Any())
            discountableItem.UnitDiscount = discountableItem.UnitPrice * (discount.Percent / 100.0m);
        foreach (var _item in group)
        {
            _item.DiscountApplied = true;
        }
    }
}