using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StarkIT.Application.Exceptions;
using StarkIT.Domain.Models;

namespace StarkIT.Infrastructure.Persistence
{
    public class DatabaseSeed
    {
        private static ILogger<DatabaseSeed>? _logger;
        private static IConfiguration? _configuration;

        public DatabaseSeed(ILogger<DatabaseSeed> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public static async Task SeedDatabaseAsync(ApplicationDbContext context)
        {
            try
            {
                if (await context.Database.EnsureCreatedAsync()) 
                {
                    if (!context.Names!.Any())
                    {
                        context.Names!.AddRange(GetDataNames());
                        await context.SaveChangesAsync();
                        _logger?.LogInformation("Database in memory is created");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while creating the database.");
                throw new Exception("An error occurred while creating the database.", ex);
            }

        }

        private static ICollection<Names> GetDataNames()
        {
            var namesDatabase = File.ReadAllText(_configuration?.GetSection("pathJsonNamesFile").Value!);
            var data = JsonConvert.DeserializeObject<ResponseDb>(namesDatabase) ?? new ResponseDb();

            if (data.Response != null && data.Response.Any())
            {
                return data.Response;
            }
            else
            {
                throw new NotFoundException();
            }

        }
    }
}
