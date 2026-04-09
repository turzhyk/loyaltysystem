namespace LoyaltySystem.Application.DTOs.Checkout;

public record CartItemRequestDto(Guid ProductId, decimal Count, decimal UnitPrice);