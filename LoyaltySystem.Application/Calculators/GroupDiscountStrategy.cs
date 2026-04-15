using LoyaltySystem.Domain.Enums;
using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;

namespace LoyaltySystem.Application.Calculators;

public class GroupDiscountStrategy : IDiscountStrategy
{
    public DiscountApplyTo ApplyTo => DiscountApplyTo.Group;

    public void Apply
        (Cart cart, Discount discount, int? limit, List<UserDiscount> userDiscounts, DateTime now)
    {
        var matchingItems = cart.Items
            .FindAll(x => (discount.ProductsId.Contains(x.ProductId) && !x.DiscountApplied));
        if (matchingItems.Count == 0)
            return;

        limit ??= (int)discount.Limit;
        int maxByCount = matchingItems.Count - matchingItems.Count % discount.GroupSize;
        int discountableItemsCount = Math.Min(maxByCount, limit ?? 0);

        if (discountableItemsCount == 0)
            return;

        var group = matchingItems
            .OrderBy(x => x.UnitPrice)
            .Take(discountableItemsCount);
        foreach (var _item in group)
        {
            _item.UnitDiscount = _item.UnitPrice * (discount.Percent / 100.0m);
            _item.DiscountApplied = true;
        }

        var productsLeft = (int)limit - discountableItemsCount;
        var correspondingUserDiscount = userDiscounts.Find(x => x.DiscountId == discount.Id);
        if (correspondingUserDiscount is null)
            userDiscounts.Add(new UserDiscount
                { Id = Guid.NewGuid(), DiscountId = discount.Id, ProductsLeft = productsLeft });
        else
        {
            correspondingUserDiscount.LastUsedAt = now;
            correspondingUserDiscount.ProductsLeft = productsLeft;
        }
    }
}