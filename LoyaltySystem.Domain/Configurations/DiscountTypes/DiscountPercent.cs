namespace LoyaltySystem.Application.Configurations.DiscountTypes;

// Discount {Percent} for all (count - count%{N}) products
public class DiscountPercent
{
    public decimal Percent { get; set; }   
    public int N { get; set; }
}