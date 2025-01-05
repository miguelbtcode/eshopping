namespace Ordering.API.Extensions;

using Infrastructure.Data;
using Infrastructure.Extensions;

public static class ApplicationMiddlewareExtensions
{
    public static void ConfigureMiddleware(this WebApplication app)
    {
        // Run database migrations
        app.MigrateDatabase<OrderContext>((context, services) =>
        {
            var logger = services.GetService<ILogger<OrderContextSeed>>();
            OrderContextSeed.SeedAsync(context, logger!).Wait();
        });
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        
        app.MapControllers();
    }
}