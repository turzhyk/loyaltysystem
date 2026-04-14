using LoyaltySystem.Infrastructure.Entities.Discount;
using LoyaltySystem.Infrastructure.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace LoyaltySystem.Infrastructure.Context;

public class ProductDbContext:DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options):base(options)
    {
        
    }
    public DbSet<DiscountEntity> GlobalDiscounts { get; set; }
    public DbSet<UserDiscountEntity> UserDiscounts { get; set; }
    public DbSet<VoucherEntity> GlobalVouchers { get; set; }
    public DbSet<UserVoucherEntity> UserVouchers { get; set; }
}