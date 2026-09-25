using LoyaltySystem.Infrastructure.Entities.Discount;
using LoyaltySystem.Infrastructure.Entities.Product;
using LoyaltySystem.Infrastructure.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace LoyaltySystem.Infrastructure.Context;

public class ProductDbContext:DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options):base(options)
    {
        ;
    }
    public DbSet<DiscountEntity> GlobalDiscounts { get; set; }
    public DbSet<CustomerDiscountEntity> CustomerDiscounts { get; set; }
    public DbSet<VoucherEntity> GlobalVouchers { get; set; }
    public DbSet<UserVoucherEntity> UserVouchers { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<CustomerDiscountEntity>(e =>
        // {
        //     e.Property(x => x.Id).IsRequired();
        // });
    }
}