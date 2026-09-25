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
            ProductsId = entity.ProductsId, StartDate = entity.StartDate, Percent = entity.Percent,
            GroupSize = entity.GroupSize, ApplyTo = entity.ApplyTo
        }).ToList();
    }

    public static List<CustomerDiscount> MapToUserDiscount(this List<CustomerDiscountEntity> entities)
    {
        if (entities == null)
            return new List<CustomerDiscount>();
        return entities.Select(entity => new CustomerDiscount
        {
            UserId = entity.UserId, DiscountId = entity.DiscountId, LastUsedAt = entity.LastUsedAt,
            ProductsLeft = entity.ProductsLeft
        }).ToList();
    }

    public static Discount MapToDiscount(this DiscountEntity entity)
    {
        if (entity == null)
            return null;
        return new Discount
        {
            Id = entity.Id, EndDate = entity.EndDate, Limit = entity.Limit, NeedActivation = entity.NeedActivation,
            ProductsId = entity.ProductsId, StartDate = entity.StartDate, Percent = entity.Percent,
            GroupSize = entity.GroupSize, ApplyTo = entity.ApplyTo
        };
    }

    public static CustomerDiscountEntity MapToEntity(this CustomerDiscount x)
    {
        if (x == null)
            return null;
        return new CustomerDiscountEntity
        {
            Id = x.Id, DiscountId = x.DiscountId, LastUsedAt = x.LastUsedAt, ProductsLeft = x.ProductsLeft,
            UserId = x.UserId, IsDeleted = false
        };
    }
    public static CustomerDiscount MapToUserDiscount(this CustomerDiscountEntity entity)
    {
        if (entity == null)
            return null;
        return new CustomerDiscount
        {
            UserId = entity.UserId, DiscountId = entity.DiscountId, LastUsedAt = entity.LastUsedAt,
            ProductsLeft = entity.ProductsLeft
        };
    }

    public static CustomerEntity MapToEntity(this User user)
    {
        return new CustomerEntity
        {
            Id = user.Id, Phone = user.Phone, IsConfirmed = user.IsConfirmed,
            CreatedAt = user.CreatedAt, Name = user.Name
        };
    }

    public static User MapToModel(this CustomerEntity entity)
    {
        return new User
        {
            Id = entity.Id, Phone = entity.Phone, IsConfirmed = entity.IsConfirmed,
            CreatedAt = entity.CreatedAt, Name = entity.Name
        };
    }
}