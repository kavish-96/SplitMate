using Microsoft.EntityFrameworkCore;
using SplitMateAPI.Data;
using SplitMateAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure SQLite Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=splitmate.db"));

// Register application services
builder.Services.AddScoped<DataService>();
builder.Services.AddScoped<BalanceService>();
builder.Services.AddScoped<SettlementService>();

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

