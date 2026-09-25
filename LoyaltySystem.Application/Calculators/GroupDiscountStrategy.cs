using LoyaltySystem.Domain.Enums;
using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;

namespace LoyaltySystem.Application.Calculators;

public class GroupDiscountStrategy : IDiscountStrategy
{
    public DiscountApplyTo ApplyTo => DiscountApplyTo.Group;

    public CustomerDiscount? Apply
        (Cart cart, Discount discount, int? limit, CustomerDiscount? customerDiscount, DateTime now)
    {
        var matchingItems = cart.Items
            .FindAll(x => (discount.ProductsId.Contains(x.ProductId) && !x.DiscountApplied));
        if (matchingItems.Count == 0)
            return null;
        
        limit ??= (int)discount.Limit;
        int maxByCount = matchingItems.Count - matchingItems.Count % discount.GroupSize;
        int discountableItemsCount = Math.Min(maxByCount, limit??1000);
        if (discountableItemsCount == 0)
            return null;

        var group = matchingItems
            .OrderBy(x => x.UnitPrice)
            .Take(discountableItemsCount);
        foreach (var item in group)
        {
            item.UnitDiscount = item.UnitPrice * (discount.Percent / 100.0m);
            item.DiscountApplied = true; 
        }

        var productsLeft = (int)limit - discountableItemsCount;



        if (customerDiscount is null)
            return new CustomerDiscount
                { Id = Guid.NewGuid(), DiscountId = discount.Id, ProductsLeft = productsLeft };
       
        {
            customerDiscount.LastUsedAt = now;
            customerDiscount.ProductsLeft = productsLeft;
            return null;
        }
    }
}