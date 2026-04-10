namespace LoyaltySystem.Domain.Models.Checkout;

public class CalculationResult
{
    public Cart NewCart { get; set; }
    public Guid UserId { get; set; }
    public IEnumerable<UserDiscount> UsedDiscounts { get; set; }
}