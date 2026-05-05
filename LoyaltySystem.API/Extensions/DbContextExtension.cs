using LoyaltySystem.Infrastructure.Context;
using LoyaltySystem.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;

namespace LoyaltySystem.API.Extensions;

public static class DbContextExtension
{
    public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserDbContext>(options => options.UseNpgsql(configuration["ConnectionStrings:UserDB"]));
        services.AddDbContext<ProductDbContext>(options => options.UseNpgsql(configuration["ConnectionStrings:ProductDB"]));
        services.AddScoped<DiscountSeeder>();
        services.AddScoped<UserSeeder>();
        
        return services;
    }
}