using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;

namespace LoyaltySystem.Application.Abstractions;

public interface ICartCalculator
{
    public Cart GetCalculated(Cart cart, List<Discount> discounts);
}