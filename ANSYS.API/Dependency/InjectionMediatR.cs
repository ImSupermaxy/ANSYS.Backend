namespace ANSYS.Application
{
    public static class InjectionMediatR
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(InjectionMediatR).Assembly);
            });

            return services;
        }
    }
}