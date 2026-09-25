using LoyaltySystem.Domain.Models.User;

namespace LoyaltySystem.Application.Abstractions;

public interface ICustomerRepository
{
    public Task<Guid?> GetIdByPhoneAsync(string phone);
    public Task<bool> UserWithIdExistsAsync(Guid id, CancellationToken cToken);

    public Task<Guid?> GetUserIdByCode(string userCode, CancellationToken cToken);
    public Task<User> GetByIdAsync(Guid id, CancellationToken cToken);

    public Task<Guid> CreateAsync(User user, CancellationToken cToken);

    public Task<int> AddPoints(Guid userId, int count, CancellationToken cToken);
}