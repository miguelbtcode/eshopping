using Discount.API.Extensions;
using Discount.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register service settings
builder.Services.ConfigureServices();

var app = builder.Build();

// Run database migrations
app.MigrateDatabase<Program>();

// Configure middleware
app.ConfigureMiddleware();

app.Run();