namespace LoyaltySystem.Application.DTOs.User;

public record UserResponseDTO(
    Guid Id,
    string Name,
    string Email,
    int Coins
);