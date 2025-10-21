using ANSYS.Application.Global.Usuarios.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace ANSYS.Application.Dependency
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            return services;
        }
    }
}