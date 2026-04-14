namespace LoyaltySystem.Application.DTOs.User;

public record UserConfirmRequestDto(Guid userId,string confirmationCode);