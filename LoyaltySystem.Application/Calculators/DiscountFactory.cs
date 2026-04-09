using LoyaltySystem.Application.Abstractions;

namespace LoyaltySystem.Application.Calculators;

public class DiscountFactory
{
    public ICartCalculator Calculator { get; set; }
}