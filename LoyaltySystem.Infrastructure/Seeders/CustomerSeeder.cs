using LoyaltySystem.Infrastructure.Context;
using LoyaltySystem.Infrastructure.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace LoyaltySystem.Infrastructure.Seeders;

public class CustomerSeeder(ProductDbContext context)
{
    public async Task Seed()
    {
        if(context.Customers.Any())
            return;
        var customerEntity = new CustomerEntity
        {
            Id = new Guid("62b64fb0-c261-4b72-a68f-3fe41053ccf3"), Email = "test@mail.com", Points = 100, Name = "Aboba",
            Phone = "111222333", CreatedAt = DateTime.UtcNow.AddDays(-10), IsConfirmed = true
        };
        await context.AddAsync(customerEntity);
        await context.SaveChangesAsync();
    }
}