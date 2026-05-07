namespace LoyaltySystem.Application.DTOs.User;

public record AddPointsRequest(Guid userId, int count);