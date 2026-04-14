namespace LoyaltySystem.Domain.Models.Discount;

public class Voucher
{
    public Guid Id { get; set; }
    public Decimal ReduceValue { get; set; }
    public Decimal ActivateThreshold { get; set; }
}