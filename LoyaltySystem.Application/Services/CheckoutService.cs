using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Application.DTOs.Checkout;
using LoyaltySystem.Application.DTOs.Discount;
using LoyaltySystem.Domain.Models.Checkout;
using LoyaltySystem.Domain.Models.Discount;
using Microsoft.Extensions.Logging;

namespace LoyaltySystem.Application.Services;

public class CheckoutService(
    IDiscountRepo discountRepo,
    ICustomerService customerService,
    ICartCalculator calculator,
    ILogger<CheckoutService> logger)
    : ICheckoutService
{
    public async Task<CartResponseDto> GetCalculatedCart(CartRequest dto, CancellationToken cToken)
    {
        Cart cart = new Cart
        {
            Items = dto.Items.Select(x => new CartItem
            {
                ProductId = x.ProductId,
                Count = x.Count,
                UnitPrice = x.UnitPrice
            }).ToList()
        };
        IEnumerable<Guid> cartItemsIds = cart.Items.Select(x => x.ProductId).ToList();

        // Guid userId = await _userService.GetUserIdBy(dto.UserCode, cToken);
        var applicableDiscounts = new List<Discount>();
        applicableDiscounts.AddRange(await discountRepo.GetByProductsAsync(cartItemsIds, cToken));
        logger.LogInformation("available {count} for the cart with {itemCount} items in it", applicableDiscounts.Count,
            cartItemsIds.Count());
        Console.WriteLine("applicable discounts: " + applicableDiscounts.Count);

        Guid? customerId = null;
        try
        {
            customerId = await customerService.GetUserIdByPersonalCodeAsync(dto.UserCode, cToken);
            Console.WriteLine($"User id is {customerId}");
        }
        catch
        {
        }
        List<CustomerDiscount>? customerDiscounts = null;
        if (customerId is Guid id)
        {
            customerDiscounts = await discountRepo.GetUserDiscountsByUserIdAsync(id, cToken);
            Console.WriteLine(customerDiscounts.Count);
        }


        var calculationResult = calculator.GetCalculated(cart, applicableDiscounts,
            customerDiscounts, DateTime.UtcNow);
        var items = calculationResult.NewCart.Items.Select(x =>
            new CartItemResponseDto(ProductId: x.ProductId, Count: x.Count,
                UnitPrice: x.UnitPrice, UnitDiscount: x.UnitDiscount)).ToList();
        var usedDiscounts = calculationResult.UsedDiscounts;


        var response = new CartResponseDto(Items: items,
            UsedDiscounts: usedDiscounts.Select(x => new CustomerDiscountDto(x.Id, x.DiscountId, x.ProductsLeft)));
        return response;
    }

    public async Task ApplyDiscounts(SaleConfirmRequest dto, CancellationToken cToken)
    {
        var usedDiscounts = dto.Discounts.Select(x => new CustomerDiscount
        {
            Id = x.Id, DiscountId = x.DiscountId, UserId = dto.UserId, ProductsLeft = x.ProductsLeft,
            LastUsedAt = DateTime.UtcNow
        }).ToList();
        await discountRepo.UpdateUserDiscounts(dto.UserId, usedDiscounts, cToken);
    }
}