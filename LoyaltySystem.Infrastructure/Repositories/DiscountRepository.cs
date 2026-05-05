using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;
using LoyaltySystem.Infrastructure.Context;
using LoyaltySystem.Infrastructure.Entities.Discount;
using LoyaltySystem.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LoyaltySystem.Infrastructure.Repositories;

public class DiscountRepository : IDiscountRepo
{
    private readonly ProductDbContext _context;


    public DiscountRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<List<Discount>> GetByProductAsync(Guid productId, CancellationToken cToken)
    {
        var result = await _context.GlobalDiscounts
            .AsNoTracking()
            .Where(i => i.ProductsId.Contains(productId))
            .ToListAsync(cToken);
        return result.MapToDiscount();
    }

    public async Task<List<UserDiscount>> GetUserDiscounts(Guid userId, CancellationToken cToken)
    {
        var result = await _context.UserDiscounts
            .AsNoTracking()
            .Where(i => i.UserId == userId && !i.IsDeleted)
            .ToListAsync(cToken);
        return result.MapToUserDiscount();
    }

    public async Task<Discount?> GetById(Guid id, CancellationToken cToken)
    {
        var result = await _context.GlobalDiscounts.
            Where(x => x.Id == id)
            .FirstOrDefaultAsync(cToken);
        return result.MapToDiscount();
    }

    public async Task<UserDiscount?> GetUserDiscountById(Guid userId, Guid discountId, CancellationToken cToken)
    {
        var result = await _context.UserDiscounts.
            Where(x => x.DiscountId == discountId && x.UserId == userId)
            .FirstOrDefaultAsync(cToken);
        return result.MapToUserDiscount();
    }

    public async Task AddUserDiscount(UserDiscount userDiscount, CancellationToken cToken)
    {
        await _context.AddAsync(userDiscount.MapToEntity());
        await _context.SaveChangesAsync();
    }
}