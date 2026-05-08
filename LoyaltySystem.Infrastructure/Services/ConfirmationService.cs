using LoyaltySystem.Application.Abstractions;

namespace LoyaltySystem.Infrastructure.Services;

public class ConfirmationService:IConfirmationService
{
    public async Task SendConfirmationRequest(Guid userId, string phone)
    {
        // send SMS with activation code
    }

    public async Task<bool> TryConfirm(Guid userId, string code)
    {
        return true;
    }
}