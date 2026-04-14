using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Domain.Models.User;
using LoyaltySystem.Infrastructure.Context;
using LoyaltySystem.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LoyaltySystem.Infrastructure.Repositories;

public class UserRepository:IUserRepository
{

    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context)
    {
        _context = context;
    }

    public async Task<Guid?> GetIdByPhone(string phone)
    {
        var result = await _context.Users.AsNoTracking().Where(u => u.Phone == phone).FirstOrDefaultAsync();
        return result.Id;
    }

    public async Task<Guid> Create(User user, CancellationToken cToken)
    {
        var entity = user.MapToUserEntity();
        await _context.Users.AddAsync(entity, cToken);
        await _context.SaveChangesAsync();
        return entity.Id;
    }
}