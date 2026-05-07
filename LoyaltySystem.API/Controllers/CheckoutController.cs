using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Application.DTOs.Checkout;
using LoyaltySystem.Application.DTOs.Discount;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltySystem.API.Controllers;

[ApiController]
[Route("api/checkout")]
public class CheckoutController:ControllerBase
{
    private readonly ICheckoutService _service;

    public CheckoutController(ICheckoutService service)
    {
        _service = service;
    }
    [HttpPost]
    public async Task<ActionResult> GetCart([FromBody] CartRequestDto dto, CancellationToken cToken)
    {
        var result = await _service.GetCalculatedCart(dto, cToken);
        return Ok(result);
    }

  
}