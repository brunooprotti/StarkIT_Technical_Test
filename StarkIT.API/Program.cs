using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using StarkIT.Infrastructure.Persistence;
using StarkIT.Infrastructure;
using StarkIT.Application;
using StarkIT.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configure and add NLog as provider
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.SetMinimumLevel(LogLevel.Trace);
});
builder.Services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();

//Add services from other layers
builder.Services.AddInfrastructureService();
builder.Services.AddAplicationServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var logger = services.GetRequiredService<ILogger<DatabaseSeed>>();
    var configuration = services.GetRequiredService<IConfiguration>();

    // Ensure database is created and seed data
    DatabaseSeed seed = new DatabaseSeed(logger, configuration);
    await DatabaseSeed.SeedDatabaseAsync(context);
}
app.Run();
