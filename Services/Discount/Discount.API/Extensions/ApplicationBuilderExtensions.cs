namespace Discount.API.Extensions;

using System.Reflection;
using Application.Handlers;
using Common.Logging;
using Core.Repositories;
using Infrastructure.Repositories;
using Serilog;

public static class ApplicationBuilderExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        // Register AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Register MediatR
        var assemblies = new[]
        {
            Assembly.GetExecutingAssembly(),
            typeof(CreateDiscountCommandHandler).Assembly
        };
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

        // Register application services
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddGrpc();
    }
    
    public static void ConfigureHost(this ConfigureHostBuilder host)
    {
        // Add serilog
        host.UseSerilog(Logging.ConfigureLogger);
    }
}