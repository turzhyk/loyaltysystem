using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Application.DTOs.Checkout;
using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;

namespace LoyaltySystem.Application.Services;

public class CheckoutService : ICheckoutService
{
    private readonly IDiscountRepo _repo;
    private readonly ICartCalculator _calculator;

    public CheckoutService(IDiscountRepo repo,  ICartCalculator calculator)
    {
        _repo = repo;
        _calculator = calculator;
    }

    public async Task<CartResponseDto> GetCalculatedCart(CartRequestDto dto, CancellationToken cToken)
    {
        Cart cart = new Cart();
        cart.Items = dto.Items.Select(x => new CartItem
        {
            ProductId = x.ProductId,
            Count = x.Count,
            UnitPrice = x.UnitPrice
        }).ToList();

        // Guid userId = await _userService.GetUserIdBy(dto.UserCode, cToken);
        var discounts = new List<Discount>();
        foreach (CartItem item in cart.Items)
        {
            var _d = await _repo.GetByProductAsync(item.ProductId, cToken);
            discounts.Concat(_d);
        }
        //  get used user discounts from repo
      
        var calculationResult = _calculator.GetCalculated(cart, discounts, 
            new List<UserDiscount>(), DateTime.UtcNow);
        var items = calculationResult.NewCart.Items.Select(x =>
            new CartItemResponseDto(ProductId: x.ProductId, Count: x.Count,
            UnitPrice: x.UnitPrice, UnitDiscount: x.UnitDiscount)).ToList();
        var usedDiscounts = calculationResult.UsedDiscounts;
        await _repo.UpdateUserDiscounts(new Guid(""), usedDiscounts, cToken);
        
        //  update used user discounts 
        return new CartResponseDto(Items: items);
    }

   
}