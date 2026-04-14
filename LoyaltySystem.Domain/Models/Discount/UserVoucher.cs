namespace LoyaltySystem.Domain.Models.Discount;

public class UserVoucher
{
    public Guid UserId { get; set; }
    public Guid VoucherId { get; set; }
    public DateTime ObtainedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
}