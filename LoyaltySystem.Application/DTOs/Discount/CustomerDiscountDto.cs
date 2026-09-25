namespace LoyaltySystem.Application.DTOs.Discount;

public record CustomerDiscountDto(
    Guid Id,
    Guid DiscountId,
    decimal ProductsLeft);