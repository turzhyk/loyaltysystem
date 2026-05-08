namespace LoyaltySystem.Application.Abstractions;

public interface IConfirmationService
{
    public Task SendConfirmationRequest(Guid userId, string phone);
    public Task<bool> TryConfirm(Guid userId, string code);

}