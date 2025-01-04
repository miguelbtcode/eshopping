namespace Discount.API.Extensions;

using Services;

public static class ApplicationMiddlewareExtensions
{
    public static void ConfigureMiddleware(this WebApplication app)
    {
        // Setting up the middleware pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseRouting();

        // Mapping gRPC services
        app.MapGrpcService<DiscountService>();

        // Default Endpoint
        app.MapGet("/", async context =>
        {
            await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client.");
        });
    }
}