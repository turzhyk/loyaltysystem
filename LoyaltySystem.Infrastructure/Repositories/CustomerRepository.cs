using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Domain.Models.User;
using LoyaltySystem.Infrastructure.Context;
using LoyaltySystem.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LoyaltySystem.Infrastructure.Repositories;

public class CustomerRepository(ProductDbContext context) : ICustomerRepository
{
    public async Task<bool> UserWithIdExistsAsync(Guid id, CancellationToken cToken)
    {
        var user = await context.Customers.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        return user is not null;
    }

    public async Task<User> GetByIdAsync(Guid id, CancellationToken cToken)
    {
        var userEntity = await context.Customers.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        if (userEntity is null)
            return null;
        return userEntity.MapToModel();
    }

    public async Task<Guid?> GetIdByPhoneAsync(string phone)
    {
        var result = await context.Customers.AsNoTracking().Where(u => u.Phone == phone).FirstOrDefaultAsync();
        if (result == null)
            return null;
        return result.Id;
    }

    public async Task<Guid> CreateAsync(User user, CancellationToken cToken)
    {
        var entity = user.MapToEntity();
        await context.Customers.AddAsync(entity, cToken);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<int> AddPoints(Guid userId, int count, CancellationToken cToken)
    {
        var userEntity = await context.Customers
            .FirstOrDefaultAsync(x => x.Id == userId, cToken);
        if (userEntity is null)
            return 0;
        userEntity!.Points += count;
        await context.SaveChangesAsync(cToken);
        return userEntity!.Points;
    }

    public async Task<Guid?> GetUserIdByCode(string userCode, CancellationToken cToken)
    {
        var result = await context.Customers.FirstOrDefaultAsync(x => x.Phone == userCode, cToken);
        if (result is null)
            return null;
        return result.MapToModel().Id;
    }

}