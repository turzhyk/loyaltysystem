using LoyaltySystem.Application.DTOs.Checkout;

namespace LoyaltySystem.Application.Abstractions;

public interface ICheckoutService
{
    public Task<CartResponseDto> GetCalculatedCart(CartRequest dto, CancellationToken cToken);

    public Task ApplyDiscounts(SaleConfirmRequest dto, CancellationToken cToken);
}