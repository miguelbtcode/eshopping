using Ordering.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register host settings
builder.Host.ConfigureHost();

// Register service settings
builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

// Configure middleware
app.ConfigureMiddleware();

app.Run();