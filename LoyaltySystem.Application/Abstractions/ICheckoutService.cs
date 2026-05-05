using LoyaltySystem.Application.DTOs.Checkout;

namespace LoyaltySystem.Application.Abstractions;

public interface ICheckoutService
{
    public Task<CartResponseDto> GetCalculatedCart(CartRequestDto dto, CancellationToken cToken);
    public Task ActivateDiscount(Guid userId, Guid discountId, CancellationToken cToken);
}