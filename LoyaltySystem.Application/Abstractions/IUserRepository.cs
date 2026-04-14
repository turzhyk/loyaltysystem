using LoyaltySystem.Domain.Models.User;

namespace LoyaltySystem.Application.Abstractions;

public interface IUserRepository
{
    public Task<Guid?> GetIdByPhone(string phone);
    public Task<Guid> Create(User user, CancellationToken cToken);
}