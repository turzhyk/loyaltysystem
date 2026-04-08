using LoyaltySystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LoyaltySystem.API.Extensions;

public static class DBContextExtension
{
    public static IServiceCollection AddDB(this IServiceCollection services)
    {
        services.AddDbContext<UserDbContext>(options => options.UseNpgsql(""));
        return services;
    }
}