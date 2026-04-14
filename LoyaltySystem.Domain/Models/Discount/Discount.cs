using System.Text.Json.Nodes;
using LoyaltySystem.Domain.Enums;

namespace LoyaltySystem.Domain.Models.Discount;

public class Discount
{
    public Guid Id { get; set; }
    public List<Guid> ProductsId { get; set; }
    public decimal Percent { get; set; }
    public DiscountApplyTo ApplyTo { get; set; }
    public decimal Limit { get; set; }
    public int GroupSize { get; set; }
    public bool NeedActivation { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double DaysLeft(DateTime now) => (EndDate - now).TotalDays;
    
    // public static Discount Create () =>
}