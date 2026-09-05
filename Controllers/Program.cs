using BusinessLogic.Interfaces;
using BusinessLogic.Mappings;
using BusinessLogic.Services;
using Controllers.Endpoints;
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
    config.AddProfile<BookingProfile>();
    config.AddProfile<AdditionalServiceProfile>();
    config.AddProfile<ReportProfile>();
});

builder.Services.AddOpenApi();

// Repo
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IAdditionalServiceRepository, AdditionalServiceRepository>();

// Services
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IAdditionalServiceService, AdditionalServiceService>();
builder.Services.AddScoped<IPriceCalculatorService, PriceCalculatorService>();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapAdditionalServiceEndpoints();
app.MapReportEndpoints();
app.MapRoomEndpoints();
app.MapBookingEndpoints();

app.Run();
