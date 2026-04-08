namespace LoyaltySystem.Infrastructure.Entities.Product;

public class ProductEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal UnitWeight { get; set; }
}