using LoyaltySystem.Domain.Models.User;

namespace LoyaltySystem.Application.Abstractions;

public interface IUserRepository
{
    public Task<Guid?> GetIdByPhone(string phone);
    public Task<bool> UserWithIdExists(Guid id, CancellationToken cToken);
    public Task<User> GetById(Guid id, CancellationToken cToken);

    public Task<Guid> Create(User user, CancellationToken cToken);
}