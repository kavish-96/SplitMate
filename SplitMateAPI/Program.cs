using SplitMateAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Register application services as Singletons (in-memory storage)
builder.Services.AddSingleton<DataService>();
builder.Services.AddSingleton<BalanceService>();
builder.Services.AddSingleton<SettlementService>();

// Configure CORS to allow Angular app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200", "http://localhost:4201", "http://localhost:4202", "http://localhost:5024")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

app.Run();

