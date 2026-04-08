using LoyaltySystem.Application.DTOs.User;

namespace LoyaltySystem.Application.Abstractions;

public interface IUserService
{
    public Task<UserResponseDTO> Get(Guid id);
}