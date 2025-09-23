using Microsoft.EntityFrameworkCore;
using MyIOTPoc.DAL.Context;

namespace MyIOTPoc.API.Setup;

/// <summary>
/// Extension methods for Entity Framework Core related configurations.
/// </summary>
public static class EntityFrameworkCoreExtensions
{
    /// <summary>
    /// Adds and configures Entity Framework Core services.
    /// </summary>
    public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services)
    {
        services.AddDbContext<IotDbContext>(options =>
            options.UseInMemoryDatabase("IotDb"));
        return services;
    }

    /// <summary>
    /// Uses and configures Entity Framework Core related middleware.
    /// </summary>
    public static WebApplication UseEntityFrameworkCore(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            using var scope = app.Services.CreateScope();

            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            Log.DatabaseReset(logger);
            
            var db = scope.ServiceProvider.GetRequiredService<IotDbContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
        return app;
    }
}

/// <summary>
/// Logger messages for Entity Framework Core related operations.
/// </summary>
public static partial class Log
{
    /// <summary>
    /// Logs a message indicating that the database is being reset in the development environment.
    /// </summary>
    /// <param name="logger"></param>
    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Information,
        Message = "Resetting database for development environment.")]
    public static partial void DatabaseReset(ILogger logger);
}