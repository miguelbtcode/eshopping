namespace Ordering.API.Extensions;

using Application.Extensions;
using Asp.Versioning;
using Common.Logging;
using EventBus.Messages.Common;
using EventBusConsumer;
using Infrastructure.Extensions;
using MassTransit;
using Microsoft.OpenApi.Models;
using Serilog;

public static class ApplicationBuilderExtensions
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
        services.AddScoped<BasketOrderingConsumerV2>();
        
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
            config.AddConsumer<BasketOrderingConsumerV2>();
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(configuration["EventBusSettings:HostAddress"]);
                // Provide the queue name with consumer settings
                cfg.ReceiveEndpoint(EventBusConstant.BasketCheckoutQueue, c =>
                {
                    c.ConfigureConsumer<BasketOrderingConsumer>(ctx);
                });
                // V2 version
                cfg.ReceiveEndpoint(EventBusConstant.BasketCheckoutQueueV2, c =>
                {
                    c.ConfigureConsumer<BasketOrderingConsumerV2>(ctx);
                });
            });
        });
        
        services.AddMassTransitHostedService();
    }
    
    public static void ConfigureHost(this ConfigureHostBuilder host)
    {
        // Add serilog
        host.UseSerilog(Logging.ConfigureLogger);
    }
}