using LoyaltySystem.Infrastructure.Context;
using LoyaltySystem.Infrastructure.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace LoyaltySystem.Infrastructure.Seeders;

public class UserSeeder
{
    private readonly UserDbContext _context;

    public UserSeeder(UserDbContext context)
    {
        _context = context;
    }

    public async Task Seed()
    {
        if(_context.Users.Any())
            return;
        var userEntity = new UserEntity
        {
            Id = new Guid("62b64fb0-c261-4b72-a68f-3fe41053ccf3"), Email = "test@mail.com", Points = 100, Name = "Aboba",
            Phone = "111222333", CreatedAt = DateTime.UtcNow.AddDays(-10), IsConfirmed = true
        };
        await _context.AddAsync(userEntity);
        await _context.SaveChangesAsync();
    }
}