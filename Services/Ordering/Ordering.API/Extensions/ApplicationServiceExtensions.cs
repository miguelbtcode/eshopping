namespace Ordering.API.Extensions;

using Application.Extensions;
using Asp.Versioning;
using EventBus.Messages.Common;
using EventBusConsumer;
using Infrastructure.Extensions;
using MassTransit;
using Microsoft.OpenApi.Models;

public static class ApplicationServiceExtensions
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add controllers
        services.AddControllers();
        
        // Add API Versioning
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        });
        
        // Application services
        services.AddApplicationServices();
        
        // Infrastructure services
        services.AddInfrastructureServices(configuration);
        
        // Consumer service
        services.AddScoped<BasketOrderingConsumer>();
        
        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Ordering.API",
                Version = "v1"
            });
        });
        
        // Add MassTransit
        services.AddMassTransit(config =>
        {
            // Mark this as consumer
            config.AddConsumer<BasketOrderingConsumer>();
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(configuration["EventBusSettings:HostAddress"]);
                // Provide the queue name with consumer settings
                cfg.ReceiveEndpoint(EventBusConstant.BasketCheckoutQueue, c =>
                {
                    c.ConfigureConsumer<BasketOrderingConsumer>(ctx);
                });
            });
        });
        
        services.AddMassTransitHostedService();
    }
}