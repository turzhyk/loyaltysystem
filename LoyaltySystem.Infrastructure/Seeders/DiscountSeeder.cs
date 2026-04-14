using LoyaltySystem.Infrastructure.Context;
using LoyaltySystem.Infrastructure.Entities.Discount;

namespace LoyaltySystem.Infrastructure.Seeders;

public class DiscountSeeder
{
    private readonly ProductDbContext _context;

    public DiscountSeeder(ProductDbContext context)
    {
        _context = context;
    }

    public async Task Seed()
    {
        await _context.GlobalDiscounts.AddAsync(new DiscountEntity
            { Id = new Guid("fab8d83a-ad8f-45ef-9b74-805d6dd1d8b0") });
    }
}