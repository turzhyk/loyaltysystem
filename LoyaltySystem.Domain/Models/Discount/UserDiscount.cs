namespace LoyaltySystem.Domain.Models.Checkout;

public class UserDiscount
{
    public Guid UserId { get; set; }
    public Guid DiscountId { get; set; }
    public decimal ProductsLeft { get; set; }
    public DateTime LastUsedAt { get; set; }
}