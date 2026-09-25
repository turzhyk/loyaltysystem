using LoyaltySystem.Domain.Enums;

namespace LoyaltySystem.Application.Calculators;

public class DiscountStrategyResolver
{
    private readonly Dictionary<DiscountApplyTo, IDiscountStrategy> _strategies;

    public DiscountStrategyResolver(IEnumerable<IDiscountStrategy> strategies)
    {
        _strategies = strategies.ToDictionary(x => x.ApplyTo);
    }

    public IDiscountStrategy Get(DiscountApplyTo applyTo)
    {
        if (!_strategies.ContainsKey(applyTo))
            throw new KeyNotFoundException($"no calculator for type {applyTo}");
        return _strategies[applyTo];
    }
}