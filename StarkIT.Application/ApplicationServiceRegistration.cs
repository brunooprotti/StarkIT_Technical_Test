using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using StarkIT.Application.Behaviours;
using StarkIT.Application.Mapper;

namespace StarkIT.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {
           
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); //Busca todas las clases que esten referenciando a abstractvalidation que es lo que crea las validaciones. Crea las instancias.
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>),typeof(LoggingPipelineBehaviour<,>));

            return services;
        }
    }
}
