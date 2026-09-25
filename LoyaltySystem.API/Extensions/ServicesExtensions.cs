using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Application.Calculators;
using LoyaltySystem.Application.Services;
using LoyaltySystem.Infrastructure.Repositories;
using LoyaltySystem.Infrastructure.Services;

namespace LoyaltySystem.API.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<Application.Abstractions.ICustomerService, CustomerService>();
        services.AddScoped<ICheckoutService, CheckoutService>();
        services.AddScoped<IConfirmationService, ConfirmationService>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IDiscountRepo, DiscountRepository>();
        services.AddScoped<ICartCalculator, CartCalculator>();
        services.AddScoped<DiscountStrategyResolver>();
        services.AddScoped<IDiscountStrategy, GroupDiscountStrategy>();
        services.AddScoped<IDiscountStrategy, CheapestOneStrategy>();
        return services;
    }
}