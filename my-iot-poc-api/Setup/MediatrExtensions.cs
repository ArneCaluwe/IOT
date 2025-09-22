namespace MyIOTPoc.API.Setup;

using MyIOTPoc.Business;

/// <summary>
/// Extension methods for setting up MediatR.
/// </summary>
public static class MediatrExtensions
{
    /// <summary>
    /// Adds and configures MediatR services.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddMediatr(this IServiceCollection services, string licenseKey)
    {
        services.AddMediatR(cfg =>
        {
            cfg.LicenseKey = licenseKey;
            cfg.RegisterServicesFromAssemblyContaining<Assembly>();
        });
        return services;
    }
}