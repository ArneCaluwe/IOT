using MyIOTPoc.DAL.Repositories;

namespace MyIOTPoc.API.Setup;

/// <summary>
/// Extension methods for setting up dependency injection.
/// </summary>
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Adds repository services to the dependency injection container.
    /// </summary>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<DeviceRepository>();
        services.AddScoped<SensorRepository>();
        return services;
    }
    // This class can be used for future EF Core related extensions if needed.
}