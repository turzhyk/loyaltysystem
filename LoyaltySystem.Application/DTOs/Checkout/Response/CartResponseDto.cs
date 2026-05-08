using LoyaltySystem.Application.DTOs.Discount;

namespace LoyaltySystem.Application.DTOs.Checkout;

public record CartResponseDto(
    List<CartItemResponseDto> Items,
    IEnumerable<UserDiscountDto> UserDiscounts
);