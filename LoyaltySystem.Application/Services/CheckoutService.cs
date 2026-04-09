using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Application.DTOs.Checkout;

namespace LoyaltySystem.Application.Services;

public class CheckoutService:ICheckoutService
{
    private readonly IDiscountRepo _repo;

    public CheckoutService(IDiscountRepo repo)
    {
        _repo = repo;
    }
    public async Task<CartResponseDto> GetCalculatedCart(CartRequestDto dto)
    {
        
        
        throw new NotImplementedException();
    }
}