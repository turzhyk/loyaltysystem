using LoyaltySystem.Domain.Enums;
using LoyaltySystem.Infrastructure.Context;
using LoyaltySystem.Infrastructure.Entities.Discount;
using Microsoft.EntityFrameworkCore;

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
        var products = new List<Guid>() { new Guid("791b2917-c466-4bfc-a33c-1125ea7632e0") };
        if (await _context.GlobalDiscounts.FirstOrDefaultAsync(x =>
                x.Id.ToString() == "fab8d83a-ad8f-45ef-9b74-805d6dd1d8b0") is null)
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
        if (await _context.GlobalDiscounts.FirstOrDefaultAsync(x =>
                x.Id.ToString() == "eefbfee7-22b5-4f2c-9e5c-b9893d72fb55") is null)
        await _context.GlobalDiscounts.AddAsync(new DiscountEntity
        {
            Id = new Guid("eefbfee7-22b5-4f2c-9e5c-b9893d72fb55"),
            ProductsId = new List<Guid>()
                { new Guid("6db3be91-c486-4809-812a-25957154a68e"), new Guid("e050ec71-aba2-47be-8955-c6ef9e861d2f") },
            ApplyTo = DiscountApplyTo.Cheapest,
            Percent = 50,
            GroupSize = 2,
            Limit = 10,
            NeedActivation = false,
            StartDate = DateTime.UtcNow.AddDays(-1),
            EndDate = DateTime.UtcNow.AddDays(1),
        });
        if (await _context.CustomerDiscounts.FirstOrDefaultAsync(x =>
                x.DiscountId.ToString() == "fab8d83a-ad8f-45ef-9b74-805d6dd1d8b0") is null)
        await _context.CustomerDiscounts.AddAsync(new CustomerDiscountEntity
        {
            DiscountId = new Guid("fab8d83a-ad8f-45ef-9b74-805d6dd1d8b0"),
            Id = Guid.NewGuid(),
            IsDeleted = false,
            UserId = new Guid("62b64fb0-c261-4b72-a68f-3fe41053ccf3"),
            LastUsedAt = DateTime.UtcNow,
            ProductsLeft = 2
        });
        foreach (var entry in _context.ChangeTracker.Entries())
        {
            Console.WriteLine(
                $"{entry.Entity.GetType().Name}: {entry.State}");
        }

        await _context.SaveChangesAsync();
    }
}