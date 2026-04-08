using System.Text.Json.Nodes;
using LoyaltySystem.Domain.Enums;

namespace LoyaltySystem.Domain.Models.Discount;

public class Discount
{
    public Guid Id { get; set; }
    public List<Guid> ProductsId { get; set; }
    public decimal Limit { get; set; }
    public DiscountType Type { get; set; }
    public JsonObject Params { get; set; }
    public bool NeedActivation { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double DaysLeft(DateTime now) => (EndDate - now).TotalDays;

}