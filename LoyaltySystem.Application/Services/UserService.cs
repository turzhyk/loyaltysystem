using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Application.DTOs.User;
using LoyaltySystem.Application.Exceptions;
using LoyaltySystem.Domain.Models.User;

namespace LoyaltySystem.Application.Services;

public class UserService:IUserService
{
    private readonly IUserRepository _repo;
    private readonly IConfirmationService _confirmationService;

    public UserService(IUserRepository repo, IConfirmationService confirmationService)
    {
        _repo = repo;
        _confirmationService = confirmationService;
    }
    public async Task<UserResponseDTO> Get(Guid id, CancellationToken cToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> GetUserIdByPhone(string phone, CancellationToken cToken)
    {
        // validate phone number
        var result = await _repo.GetIdByPhone(phone);
        if (result == null)
            throw new UserNotFoundException();
        return result.Value;
    }

    public async Task<Guid> Create(UserCreateRequestDto dto, CancellationToken cToken)
    {
        var phoneNumber = dto.phoneNumber;
        var user = new User { Id = Guid.NewGuid(), Phone = phoneNumber, IsConfirmed = false};
        var result = _repo.Create(user, cToken);
        await _confirmationService.SendCofirmationRequest(user.Id, phoneNumber);
        return user.Id;
    }
    
    public async Task<string> Confirm(UserConfirmRequestDto dto, CancellationToken cToken)
    {
        var result = await _confirmationService.TryConfirm(dto.userId, dto.confirmationCode);
        throw new NotImplementedException();
    }
}