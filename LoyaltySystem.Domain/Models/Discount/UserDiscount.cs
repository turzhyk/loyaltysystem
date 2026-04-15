namespace LoyaltySystem.Domain.Models.Discount;

public class UserDiscount
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid DiscountId { get; set; }
    public decimal ProductsLeft { get; set; }
    public DateTime LastUsedAt { get; set; }
}