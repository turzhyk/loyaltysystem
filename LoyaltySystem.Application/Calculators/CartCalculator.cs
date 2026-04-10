using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Domain.Enums;
using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;

namespace LoyaltySystem.Application.Calculators;

public class CartCalculator : ICartCalculator
{
    public CalculationResult GetCalculated(Cart cart, List<Discount> discounts, Guid userId,
        List<UserDiscount> userDiscounts)
    {
        CalculationResult result = new CalculationResult { UserId = userId };
        Cart newCart = new Cart();
        List<UserDiscount> usedDiscounts = new List<UserDiscount>();
        newCart.Items = new List<CartItem>();
        foreach (var discount in discounts)
        {
            decimal limit = 0;
            if (discount.NeedActivation)
            {
                if (userDiscounts.Find(x => x.DiscountId == discount.Id) != null)
                {
                    limit = userDiscounts.Find(x => x.DiscountId == discount.Id).ProductsLeft;
                }
                else
                {
                    continue;
                }
            }

            switch (discount.ApplyTo)
            {
                case DiscountApplyTo.Group:

                    var items = cart.Items
                        .FindAll(x => (discount.ProductsId.Contains(x.ProductId) && !x.DiscountApplied));
                    if (items.Count == 0)
                        continue;
                    var itemsCount = items.Count - items.Count % discount.GroupSize;
                    limit = limit == 0 ? discount.Limit : limit;

                    while (itemsCount > discount.Limit)
                        itemsCount -= discount.GroupSize;

                    var group = items
                        .OrderBy(x => x.UnitPrice)
                        .Take(itemsCount);
                    foreach (var _item in group)
                    {
                        _item.UnitDiscount = _item.UnitPrice * (discount.Percent / 100.0m);
                        _item.DiscountApplied = true;
                        newCart.Items.Add(_item);
                    }

                    // usedDiscounts.Add(new UserDiscount { DiscountId = discount.Id, ProductsLeft = limit - itemsCount });
                    break;
                case DiscountApplyTo.Weight:
                    var weightedItem =
                        cart.Items.Find(x => discount.ProductsId[0] == x.ProductId && !x.DiscountApplied);
                    if (weightedItem == null)
                        continue;
                    if (weightedItem.Count > discount.Limit)
                    {
                        var undiscountedItem = new CartItem
                            { ProductId = weightedItem.ProductId, Count = weightedItem.Count - discount.Limit };
                        newCart.Items.Add(undiscountedItem);
                    }

                    weightedItem.UnitDiscount = weightedItem.UnitPrice * (discount.Percent / 100);
                    weightedItem.DiscountApplied = true;
                    newCart.Items.Add(weightedItem);
                    // usedDiscounts.Add(new UserDiscount { DiscountId = discount.Id, TimesUsed = weightedItem.Count });
                    break;
            }
        }

        result.NewCart = newCart;
        result.UsedDiscounts = usedDiscounts;
        throw new NotImplementedException();
    }
}