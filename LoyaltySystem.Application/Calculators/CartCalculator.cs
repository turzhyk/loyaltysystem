using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;

namespace LoyaltySystem.Application.Calculators;

public class CartCalculator:ICartCalculator
{
    public Cart GetCalculated(Cart cart, List<Discount> discounts)
    {
        foreach (var item in cart.Items)
        {
            var unitDiscount = 0;
            var count = item.Count;
            
            foreach (var discount in discounts)
            {
                if (discount.ProductsId.Contains(item.ProductId) )
                {
                    if (discount.ProductsId.Count == 1)
                    {
                        
                    }
                    else
                    {
                       
                    }
                    item.DiscountApplied = true;
                }
            }
        }
        throw new NotImplementedException();
    }
}