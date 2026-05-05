namespace LoyaltySystem.Application.DTOs.Discount;

public record ActivateDiscountRequest(Guid userId, Guid discountId);