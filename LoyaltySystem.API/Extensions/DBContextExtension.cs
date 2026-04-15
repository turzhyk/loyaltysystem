using LoyaltySystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LoyaltySystem.API.Extensions;

public static class DBContextExtension
{
    public static IServiceCollection AddDB(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserDbContext>(options => options.UseNpgsql(configuration["ConnectionStrings:UserDB"]));
        services.AddDbContext<ProductDbContext>(options => options.UseNpgsql(configuration["ConnectionStrings:ProductDB"]));
        return services;
    }
}