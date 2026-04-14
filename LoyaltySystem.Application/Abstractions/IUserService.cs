using LoyaltySystem.Application.DTOs.User;

namespace LoyaltySystem.Application.Abstractions;

public interface IUserService
{
    public Task<UserResponseDTO> Get(Guid id, CancellationToken cToken);
    public Task<Guid> GetUserIdByPhone(string phone, CancellationToken cToken);
    public Task<Guid> Create(UserCreateRequestDto dto, CancellationToken cToken);
    public Task<string> Confirm(UserConfirmRequestDto dto, CancellationToken cToken);
}