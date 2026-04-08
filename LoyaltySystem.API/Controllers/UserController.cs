using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Application.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltySystem.API.Controllers;
[ApiController]
[Route("api/users")]
public class UserController :ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseDTO>> GetUser(Guid id)
    {
        var result = await _userService.Get(id);
        return result;
    }
}