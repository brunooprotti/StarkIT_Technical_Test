using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StarkIT.Domain.Models;

namespace StarkIT.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApplicationDbContext> _logger;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration, ILogger<ApplicationDbContext> logger) : base(options)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public DbSet<Names>? Names { get; set; }

    }
}
