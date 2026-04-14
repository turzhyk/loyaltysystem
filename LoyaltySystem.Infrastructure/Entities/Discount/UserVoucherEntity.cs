namespace LoyaltySystem.Infrastructure.Entities.Discount;

public class UserVoucherEntity
{
    public Guid UserId { get; set; }
    public Guid VoucherId { get; set; }
    public DateTime ObtainedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; }
}