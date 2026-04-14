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
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICheckoutService, CheckoutService>();
        services.AddScoped<IConfirmationService, ConfirmationService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDiscountRepo, DiscountRepository>();
        services.AddScoped<ICartCalculator, CartCalculator>();
        return services;
    }
}