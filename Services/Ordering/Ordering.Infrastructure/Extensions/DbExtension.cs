namespace Ordering.Infrastructure.Extensions;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;

public static class DbExtension
{
    public static void MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder)
        where TContext : DbContext
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<TContext>>();
        var context = services.GetService<TContext>();

        try
        {
            logger.LogInformation("Started Db Migration: {contextName}", typeof(TContext).Name);
            //retry strategy
            var retry = Policy.Handle<SqlException>()
                .WaitAndRetry(
                    retryCount: 5,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, span, count) =>
                    {
                        logger.LogError("Retrying because of exception: {Exception}, span: {Span}", exception, span);
                    });
            retry.Execute(() => CallSeeder(seeder, context, services));
            logger.LogInformation("Migration Completed: {ContextName}", typeof(TContext).Name);
        }
        catch (Exception ex) 
        {
            logger.LogError(ex, "An error occurred while migrating db: {ContextName}", typeof(TContext).Name);
        }
    }

    private static void CallSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext? context, IServiceProvider services) where TContext : DbContext
    {
        context!.Database.Migrate();
        seeder(context, services);
    }
}