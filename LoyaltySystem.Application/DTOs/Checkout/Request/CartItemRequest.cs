namespace LoyaltySystem.Application.DTOs.Checkout;

public record CartItemRequest(Guid ProductId, decimal Count, decimal UnitPrice);