using Basket.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register service settings
builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

// Configure middleware
app.ConfigureMiddleware();

app.Run();