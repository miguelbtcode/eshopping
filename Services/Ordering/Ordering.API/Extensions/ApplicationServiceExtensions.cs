namespace Ordering.API.Extensions;

using Application.Extensions;
using Asp.Versioning;
using Infrastructure.Extensions;
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
    }
}