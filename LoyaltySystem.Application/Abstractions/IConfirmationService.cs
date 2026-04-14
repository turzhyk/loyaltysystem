namespace LoyaltySystem.Application.Abstractions;

public interface IConfirmationService
{
    public Task SendCofirmationRequest(Guid userId, string phone);
    public Task<bool> TryConfirm(Guid userId, string code);

}