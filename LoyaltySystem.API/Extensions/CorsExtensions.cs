namespace LoyaltySystem.API.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection AddCorsPolicies(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: "_allow",
                policy => { policy.WithOrigins("localhost:3000"); });
        });
        return services;
    }
}