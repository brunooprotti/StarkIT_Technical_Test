using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarkIT.Application.Contracts.Persistence;
using StarkIT.Infrastructure.Persistence;
using StarkIT.Infrastructure.Repositories;

namespace StarkIT.Infrastructure
{
    public static class InfraestructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>  options.UseInMemoryDatabase("UserDatabase"));

            services.AddScoped(typeof(IBaseRepository<>),typeof(BaseRepository<>));
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddTransient<DatabaseSeed>();

            return services;
        }
    }
}
