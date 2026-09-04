using BusinessLogic.Interfaces;
using BusinessLogic.Mappings;
using BusinessLogic.Services;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var dbConfig = builder.Configuration.GetSection("DatabaseConfig");
var connectionStringBuilder = new NpgsqlConnectionStringBuilder
{
    Host = dbConfig["Host"],
    Port = int.Parse(dbConfig["Port"] ?? "5432"),
    Database = dbConfig["Database"],
    Username = dbConfig["Username"],

    // Inside real project we can use Secrets or Azure Key
    Password = "0000"
};

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionStringBuilder.ConnectionString);
});

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<RoomProfile>();
});

builder.Services.AddOpenApi();

builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IPriceCalculatorService, PriceCalculatorService>();
builder.Services.AddScoped<IAdditionalServiceService, AdditionalServiceService>();

builder.Services.AddScoped<IAdditionalServiceRepository, AdditionalServiceRepository>();

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();
var app = builder.Build();

app.Run();
