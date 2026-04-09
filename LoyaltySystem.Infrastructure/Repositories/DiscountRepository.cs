using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Domain.Models.Discount;
using LoyaltySystem.Infrastructure.Context;
using LoyaltySystem.Infrastructure.Entities.Discount;
using Microsoft.EntityFrameworkCore;

namespace LoyaltySystem.Infrastructure.Repositories;

public class DiscountRepository:IDiscountRepo
{
    private readonly ProductDbContext _context;

    private static Discount MapToDiscount(DiscountEntity entity) => new Discount
    {
        Id = entity.Id, EndDate = entity.EndDate, Limit = entity.Limit, NeedActivation = entity.NeedActivation,
        Params = entity.Params, ProductsId = entity.ProductsId, StartDate = entity.StartDate, Type = entity.Type
    };
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
        return result.Select(x => MapToDiscount(x)).ToList();
        throw new NotImplementedException();
    }
}