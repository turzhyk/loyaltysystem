using System.Text.Json.Nodes;
using LoyaltySystem.Domain.Enums;

namespace LoyaltySystem.Infrastructure.Entities.Discount;

public class DiscountEntity
{
    public Guid Id { get; set; }
    public List<Guid> ProductsId { get; set; }
    public decimal Limit { get; set; }

    public bool NeedActivation { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsDeleted { get; set; }
}