namespace LoyaltySystem.Application.DTOs.Checkout;

public record CartRequestDto(List<CartItemDto> Items,string UserCode);