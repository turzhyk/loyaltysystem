using LoyaltySystem.Domain.Enums;
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
        if (_context.GlobalDiscounts.Any())
            return;
        var products = new List<Guid>() { new Guid("791b2917-c466-4bfc-a33c-1125ea7632e0") };
        await _context.GlobalDiscounts.AddAsync(new DiscountEntity
        {
            Id = new Guid("fab8d83a-ad8f-45ef-9b74-805d6dd1d8b0"),
            ProductsId = products,
            ApplyTo = DiscountApplyTo.Group,
            Percent = 50,
            GroupSize = 2,
            Limit = 10,
            NeedActivation = false,
            StartDate = DateTime.UtcNow.AddDays(-1),
            EndDate = DateTime.UtcNow.AddDays(1),
        });
        await _context.SaveChangesAsync();
    }
}