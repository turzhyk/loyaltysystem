namespace LoyaltySystem.Domain.Enums;

public enum DiscountType
{
    Percent, // Discount % for product
    NPercent, // Discount % for N product of same Id
    NFree // N product of same Id is free
}