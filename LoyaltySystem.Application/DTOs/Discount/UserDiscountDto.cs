namespace LoyaltySystem.Application.DTOs.Discount;

public record UserDiscountDto(
    Guid Id,
    Guid DiscountId,
    decimal ProductsLeft);