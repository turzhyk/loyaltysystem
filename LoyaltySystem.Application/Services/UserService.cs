using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Application.DTOs.User;
using LoyaltySystem.Application.Exceptions;
using LoyaltySystem.Domain.Models.Discount;
using LoyaltySystem.Domain.Models.User;

namespace LoyaltySystem.Application.Services;

public class UserService:IUserService
{
    private readonly IUserRepository _repo;
    private readonly IDiscountRepo _discountRepo;
    private readonly IConfirmationService _confirmationService;

    public UserService(IUserRepository repo, IDiscountRepo discountRepo,  IConfirmationService confirmationService)
    {
        _repo = repo;
        _discountRepo = discountRepo;
        _confirmationService = confirmationService;
    }
    public async Task<UserResponseDTO> Get(Guid id, CancellationToken cToken)
    {

        var exists = await _repo.UserWithIdExists(id, cToken);
        if (!exists)
            throw new UserNotFoundException();
        var user = await _repo.GetById(id, cToken);
        return new UserResponseDTO(user.Id, user.Name, user.Email, 0);
    }

    public async Task<Guid> GetUserIdByPhone(string phone, CancellationToken cToken)
    {
        // validate phone number
        var result = await _repo.GetIdByPhone(phone);
        if (result == null)
            throw new UserNotFoundException();
        return result.Value;
    }
    public async Task<Guid> GetUserIdByPersonalCode(string phone, CancellationToken cToken)
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
    
    public async Task ActivateDiscount(Guid userId, Guid discountId, CancellationToken cToken)
    {
        var discount = await _discountRepo.GetById(discountId, cToken);
        if (discount is null)
            throw new KeyNotFoundException($"Discount with id {discountId} does not exist");
        bool isActivated = await _discountRepo.GetUserDiscountById(userId, discountId, cToken) is not null;
        if (isActivated)
            throw new Exception("Discount is already active");
        var userDiscount = new UserDiscount
        {
            Id = Guid.NewGuid(), DiscountId = discountId, UserId = userId, LastUsedAt = DateTime.UtcNow,
            ProductsLeft = discount.Limit
        };
        await _discountRepo.AddUserDiscount(userDiscount, cToken);
    }

    public async Task<int> AddPoints(Guid userId, int count, CancellationToken cToken)
    {
        var exists = await _repo.UserWithIdExists(userId, cToken);
        if (!exists)
            throw new UserNotFoundException();
        return await _repo.AddPoints(userId, count, cToken);
    }
}