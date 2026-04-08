using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Application.DTOs.User;

namespace LoyaltySystem.Application.Services;

public class UserService:IUserService
{
    public async Task<UserResponseDTO> Get(Guid id)
    {
        throw new NotImplementedException();
    }
}