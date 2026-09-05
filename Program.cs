using FlightTracker.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<FlightTracker.Entities.AppContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("FlightTrackerContext") ?? throw new InvalidOperationException("Connection string 'FlightTrackerContext' not found.")));
builder.Services.AddControllers();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IMissionService, MissionService>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();