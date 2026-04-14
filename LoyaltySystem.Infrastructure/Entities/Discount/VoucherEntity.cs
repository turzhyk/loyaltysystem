namespace LoyaltySystem.Infrastructure.Entities.Discount;

public class VoucherEntity
{
    public Guid Id { get; set; }
    public Decimal ReduceValue { get; set; }
    public Decimal ActivateThreshold { get; set; }
    public bool IsDeleted { get; set; }
}