using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Application.DTOs.Checkout;
using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;

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
      
        var calculatedCart = _calculator.GetCalculated(cart, discounts, 
            new List<UserDiscount>(), DateTime.UtcNow).NewCart;
        var items = calculatedCart.Items.Select(x =>
            new CartItemResponseDto(ProductId: x.ProductId, Count: x.Count,
            UnitPrice: x.UnitPrice, UnitDiscount: x.UnitDiscount)).ToList();
        
        //  update used user discounts 
        return new CartResponseDto(Items: items);
    }

    public async Task ActivateDiscount(Guid userId, Guid discountId, CancellationToken cToken)
    {
        var discount = await _repo.GetById(discountId, cToken);
        if (discount is null)
            throw new KeyNotFoundException($"Discount with id {discountId} does not exist");
        bool isActivated = await _repo.GetUserDiscountById(userId, discountId, cToken) is not null;
        if (isActivated)
            throw new Exception("Discount is already active");
        var userDiscount = new UserDiscount
        {
            Id = Guid.NewGuid(), DiscountId = discountId, UserId = userId, LastUsedAt = DateTime.UtcNow,
            ProductsLeft = discount.Limit
        };
        await _repo.AddUserDiscount(userDiscount, cToken);
    }
}