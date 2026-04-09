namespace LoyaltySystem.Application.DTOs.Checkout;

public record CartItemResponseDto(Guid ProductId, decimal Count, decimal UnitPrice, decimal UnitDiscount);