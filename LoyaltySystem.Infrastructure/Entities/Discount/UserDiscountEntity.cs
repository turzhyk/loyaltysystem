namespace LoyaltySystem.Infrastructure.Entities.Discount;

public class UserDiscountEntity
{
    public Guid UserId { get; set; }
    public Guid DiscountId { get; set; }
    public decimal TimesUsed { get; set; }
    public DateTime LastUsedAt { get; set; }
    public bool IsDeleted { get; set; }
}