namespace LoyaltySystem.Application.DTOs.Checkout;

public record CartRequest(List<CartItemRequest> Items,string UserCode);