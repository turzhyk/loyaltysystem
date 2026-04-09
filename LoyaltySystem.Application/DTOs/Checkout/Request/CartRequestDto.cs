namespace LoyaltySystem.Application.DTOs.Checkout;

public record CartRequestDto(List<CartItemRequestDto> Items,string UserCode);