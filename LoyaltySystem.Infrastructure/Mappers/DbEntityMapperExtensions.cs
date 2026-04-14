using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;
using LoyaltySystem.Domain.Models.User;
using LoyaltySystem.Infrastructure.Entities.Discount;
using LoyaltySystem.Infrastructure.Entities.User;

namespace LoyaltySystem.Infrastructure.Mappers;

public static class DbEntityMapperExtensions
{
    public static List<Discount> MapToDiscount(this List<DiscountEntity> entities)
    {
        if (entities == null)
            return new List<Discount>();
        return entities.Select(entity => new Discount
        {
            Id = entity.Id, EndDate = entity.EndDate, Limit = entity.Limit, NeedActivation = entity.NeedActivation,
            ProductsId = entity.ProductsId, StartDate = entity.StartDate
        }).ToList();
    }

    public static List<UserDiscount> MapToUserDiscount(this List<UserDiscountEntity> entities)
    {
        if (entities == null)
            return new List<UserDiscount>();
        return entities.Select(entity => new UserDiscount
        {
            UserId = entity.UserId, DiscountId = entity.DiscountId, LastUsedAt = entity.LastUsedAt,
            ProductsLeft = entity.ProductsLeft
        }).ToList();
    }

    public static UserEntity MapToUserEntity(this User user)
    {
        return new UserEntity
        {
            Id = user.Id, Phone = user.Phone, IsConfirmed = user.IsConfirmed,
            CreatedAt = user.CreatedAt, Name = user.Name
        };
    }
}