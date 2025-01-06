namespace Basket.API.Extensions;

using System.Reflection;
using Application.GrpcService;
using Application.Handlers;
using Asp.Versioning;
using Core.Repositories;
using Discount.Grpc.Protos;
using Infrastructure.Repositories;
using MassTransit;
using Microsoft.OpenApi.Models;

public static class ApplicationServiceExtensions
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add controllers
        services.AddControllers();
        
        // Add API versioning
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });
        
        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Basket.API",
                Version = "v1"
            });
        });
        
        // Register AutoMapper
        services.AddAutoMapper(typeof(Program).Assembly);

        // Register MediatR
        var assembies = new[]
        {
            Assembly.GetExecutingAssembly(),
            typeof(CreateShoppingCartCommandHandler).Assembly
        };

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembies));
        
        // Redis configuration
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
        });
        
        // Application Services
        services.AddScoped<IBasketRepository, BasketRepository>();
        
        // Grpc client
        services.AddScoped<DiscountGrpcService>();
        services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
            (cfg => cfg.Address = new Uri(configuration["GrpcSettings:DiscountUrl"]!));
        
        // Add MassTransit
        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((ct, cfg) =>
            {
                cfg.Host(configuration["EventBusSettings:HostAddress"]);
            });
        });

        services.AddMassTransitHostedService();
    }
}