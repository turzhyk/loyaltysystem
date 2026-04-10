using LoyaltySystem.Infrastructure.Entities.Discount;
using LoyaltySystem.Infrastructure.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace LoyaltySystem.Infrastructure.Context;

public class ProductDbContext:DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options):base(options)
    {
        
    }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<DiscountEntity> GlobalDiscounts { get; set; }
    public DbSet<UserDiscountEntity> UserDiscounts { get; set; }
}