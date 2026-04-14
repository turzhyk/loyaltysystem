using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Application.DTOs.Checkout;
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
    public async Task<ActionResult> GetCart([FromBody] CartRequestDto dto)
    {
        return Ok();
    }
}