using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Application.DTOs.Discount;
using LoyaltySystem.Application.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltySystem.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly ICustomerService _icUstomerService;

    public UserController(ICustomerService icUstomerService)
    {
        _icUstomerService = icUstomerService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseDTO>> GetUser(Guid id, CancellationToken cToken)
    {
        var result = await _icUstomerService.Get(id, cToken);
        return result;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] UserCreateRequestDto dto, CancellationToken cToken)
    {
        await _icUstomerService.Create(dto, cToken);
        return Ok();
    }

    [HttpPost("confirm")]
    public async Task<ActionResult<string>> Confirm([FromBody] UserConfirmRequestDto dto, CancellationToken cToken)
    {
        var result = await _icUstomerService.Confirm(dto, cToken);
        return Ok(result);
    }

    [HttpPost("discount")]
    public async Task<ActionResult> ActivateDiscount([FromBody] ActivateDiscountRequest dto, CancellationToken cToken)
    {
        await _icUstomerService.ActivateDiscount(dto.userId, dto.discountId, cToken);
        return Ok();
    }

    [HttpPost("{id}/points")]
    public async Task<ActionResult<int>> AddUserPoints(string id, [FromBody] AddPointsRequest dto,
        CancellationToken cToken)
    {
        var result = _icUstomerService.AddPoints(new Guid(id), dto.count, cToken);
        return Ok(result);
    }
}