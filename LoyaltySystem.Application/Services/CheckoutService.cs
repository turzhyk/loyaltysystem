using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Application.DTOs.Checkout;
using LoyaltySystem.Application.DTOs.Discount;
using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;

namespace LoyaltySystem.Application.Services;

public class CheckoutService : ICheckoutService
{
    private readonly IDiscountRepo _repo;
    private readonly ICartCalculator _calculator;

    public CheckoutService(IDiscountRepo repo, ICartCalculator calculator)
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
        var applicableDiscounts = new List<Discount>();
        var _itemIds = cart.Items.Select(x => x.ProductId).ToList();
        applicableDiscounts.AddRange(await _repo.GetByProductsAsync(_itemIds, cToken));
        Console.WriteLine("applicable discounts: "+applicableDiscounts.Count);

        //  get used user discounts from repo

        var calculationResult = _calculator.GetCalculated(cart, applicableDiscounts,
            new List<UserDiscount>(), DateTime.UtcNow);
        var items = calculationResult.NewCart.Items.Select(x =>
            new CartItemResponseDto(ProductId: x.ProductId, Count: x.Count,
                UnitPrice: x.UnitPrice, UnitDiscount: x.UnitDiscount)).ToList();
        var usedDiscounts = calculationResult.UsedDiscounts;


        var response = new CartResponseDto(Items: items,
            UserDiscounts: usedDiscounts.Select(x => new UserDiscountDto(x.Id, x.DiscountId, x.ProductsLeft)));
        return response;
    }

    public async Task ApplyDiscounts(SaleConfirmRequest dto, CancellationToken cToken)
    {
        var usedDiscounts = dto.Discounts.Select(x => new UserDiscount
        {
            Id = x.Id, DiscountId = x.DiscountId, UserId = dto.UserId, ProductsLeft = x.ProductsLeft,
            LastUsedAt = DateTime.UtcNow
        }).ToList();
        await _repo.UpdateUserDiscounts(dto.UserId, usedDiscounts, cToken);
    }
}