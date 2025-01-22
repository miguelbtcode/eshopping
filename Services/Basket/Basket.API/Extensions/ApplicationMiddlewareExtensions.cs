namespace Basket.API.Extensions;

public static class ApplicationMiddlewareExtensions
{
    public static void ConfigureMiddleware(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API v1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "Basket.API v2");
            });
        }
        
        app.UseCors("CorsPolicy");

        app.UseAuthorization();

        app.MapControllers();
    }
}