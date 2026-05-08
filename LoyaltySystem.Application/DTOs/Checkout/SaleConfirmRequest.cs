using LoyaltySystem.Application.DTOs.Discount;

namespace LoyaltySystem.Application.DTOs.Checkout;

public record SaleConfirmRequest(
    Guid UserId,
    IEnumerable<UserDiscountDto> Discounts
);