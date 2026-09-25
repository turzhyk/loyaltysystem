using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Application.DTOs.User;
using LoyaltySystem.Application.Exceptions;
using LoyaltySystem.Domain.Models.Discount;
using LoyaltySystem.Domain.Models.User;

namespace LoyaltySystem.Application.Services;

public class CustomerService(
    ICustomerRepository repo,
    IDiscountRepo discountRepo,
    IConfirmationService confirmationService)
    : ICustomerService
{
    public async Task<UserResponseDTO> Get(Guid id, CancellationToken cToken)
    {

        var exists = await repo.UserWithIdExistsAsync(id, cToken);
        if (!exists)
            throw new UserNotFoundException();
        var user = await repo.GetByIdAsync(id, cToken);
        return new UserResponseDTO(user.Id, user.Name, user.Email, 0);
    }
    
    public async Task<Guid> GetUserIdByPersonalCodeAsync(string phone, CancellationToken cToken)
    {
        // validate phone number
        var result = await repo.GetIdByPhoneAsync(phone);
        if (result == null)
            throw new UserNotFoundException();
        return result.Value;
    }
    
    public async Task<Guid> Create(UserCreateRequestDto dto, CancellationToken cToken)
    {
        var phoneNumber = dto.phoneNumber;
        var user = new User { Id = Guid.NewGuid(), Phone = phoneNumber, IsConfirmed = false};
        var result = repo.CreateAsync(user, cToken);
        await confirmationService.SendConfirmationRequest(user.Id, phoneNumber);
        return user.Id;
    }
    
    public async Task<string> Confirm(UserConfirmRequestDto dto, CancellationToken cToken)
    {
        var result = await confirmationService.TryConfirm(dto.userId, dto.confirmationCode);
        throw new NotImplementedException();
    }
    
    public async Task ActivateDiscount(Guid userId, Guid discountId, CancellationToken cToken)
    {
        var discount = await discountRepo.GetById(discountId, cToken);
        if (discount is null)
            throw new KeyNotFoundException($"Discount with id {discountId} does not exist");
        bool isActivated = await discountRepo.GetUserDiscountById(userId, discountId, cToken) is not null;
        if (isActivated)
            throw new Exception("Discount is already active");
        var userDiscount = new CustomerDiscount
        {
            Id = Guid.NewGuid(), DiscountId = discountId, UserId = userId, LastUsedAt = DateTime.UtcNow,
            ProductsLeft = discount.Limit
        };
        await discountRepo.AddUserDiscount(userDiscount, cToken);
    }

    public async Task<int> AddPoints(Guid userId, int count, CancellationToken cToken)
    {
        var exists = await repo.UserWithIdExistsAsync(userId, cToken);
        if (!exists)
            throw new UserNotFoundException();
        return await repo.AddPoints(userId, count, cToken);
    }
}