namespace Discount.API.Extensions;

using System.Reflection;
using Application.Handlers;
using Core.Repositories;
using Infrastructure.Repositories;

public static class ApplicationServiceExtensions
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
}