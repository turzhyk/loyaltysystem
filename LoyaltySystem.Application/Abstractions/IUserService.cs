using LoyaltySystem.Application.DTOs.User;

namespace LoyaltySystem.Application.Abstractions;

public interface IUserService
{
    public Task<UserResponseDTO> Get(Guid id, CancellationToken cToken);
    public Task<Guid> GetUserIdByPersonalCode(string phone, CancellationToken cToken);
    public Task<Guid> Create(UserCreateRequestDto dto, CancellationToken cToken);
    public Task<string> Confirm(UserConfirmRequestDto dto, CancellationToken cToken);

    public Task ActivateDiscount(Guid userId, Guid discountId, CancellationToken cToken);
    public Task<int> AddPoints(Guid userId, int count, CancellationToken cToken);
    

}