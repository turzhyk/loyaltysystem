using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Application.DTOs.Checkout;
using LoyaltySystem.Domain.Models.Checkout;

namespace LoyaltySystem.Application.Services;

public class CheckoutService : ICheckoutService
{
    private readonly IDiscountRepo _repo;
    private readonly IUserService _userService;
    private readonly ICartCalculator _calculator;

    public CheckoutService(IDiscountRepo repo, IUserService userService, ICartCalculator calculator)
    {
        _repo = repo;
        _userService = userService;
        _calculator = calculator;
    }

    public async Task<CartResponseDto> GetCalculatedCart(CartRequestDto dto, CancellationToken cToken)
    {
        Cart cart = new Cart();
        cart.Items = dto.Items.Select(x => new CartItem
            { ProductId = x.ProductId, Count = x.Count, UnitPrice = x.UnitPrice }).ToList();

        Guid userId = await _userService.GetUserIdByPhone(dto.UserCode, cToken);
        //  get used user discounts from repo
        //  get global discounts that apply to items in a cart
        
        //  calculate
        
        //  update used user discounts 
        throw new NotImplementedException();
    }
}