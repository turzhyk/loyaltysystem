using LoyaltySystem.Application.DTOs.Checkout;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltySystem.API.Controllers;

[ApiController]
[Route("api/checkout")]
public class CheckoutController:ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> GetCart([FromBody] CartRequestDto dto)
    {
        return Ok();
    }
}