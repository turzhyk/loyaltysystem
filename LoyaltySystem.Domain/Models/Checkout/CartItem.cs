namespace LoyaltySystem.Domain.Models.Checkout;

public class CartItem
{
    public Guid ProductId { get; set; }
    public decimal Count { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal UnitDiscount { get; set; }
    public bool DiscountApplied { get; set; }
}