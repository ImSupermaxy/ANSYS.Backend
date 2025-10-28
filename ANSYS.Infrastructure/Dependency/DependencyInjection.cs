using ANSYS.Application.Global.PedidoItens.Mappers;
using ANSYS.Application.Global.Pedidos.Commands;
using ANSYS.Application.Global.Pedidos.Mappers;
using ANSYS.Application.Global.Usuarios.Commands;
using ANSYS.Application.Global.Usuarios.Mappers;
using ANSYS.Domain.Abstractions.Context.EntityFramework;
using ANSYS.Domain.Abstractions.Mappers;
using ANSYS.Domain.Global.PedidoItens.Repositories;
using ANSYS.Domain.Global.Pedidos.Entities;
using ANSYS.Domain.Global.Pedidos.Repositories;
using ANSYS.Domain.Global.Usuarios.Entities;
using ANSYS.Domain.Global.Usuarios.Repositories;
using ANSYS.Infrastructure.Context;
using ANSYS.Infrastructure.Context.EntityFramework;
using ANSYS.Infrastructure.Global.PedidoItens;
using ANSYS.Infrastructure.Global.Pedidos;
using ANSYS.Infrastructure.Global.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ANSYS.Dependency.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            AddPersistence(services, configuration);

            return services;
        }

        private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
        {
            string cnnStringPostgres = configuration.GetConnectionString("Postgresql") ??
                                      throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<AnsysEntityFrameworkContext>(options => options.UseNpgsql(cnnStringPostgres));

            //Entitys
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IEntityMapper<Usuario, UsuarioCommandInsert, UsuarioCommandUpdate>, UsuarioMapper>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IEntityMapper<Pedido, PedidoCommandInsert, PedidoCommandUpdate>, PedidoMapper>();
            services.AddScoped<IPedidoItemRepository, PedidoItemRepository>();
            services.AddScoped<PedidoItemMapper, PedidoItemMapper>();

            //Context
            services.AddScoped<IEntityFrameworkDBContext>(sp => sp.GetRequiredService<AnsysEntityFrameworkContext>());

            //Configuração de qual banco de dados será usado...
            DatabaseRunModeConfiguration.UseLocalDatabase(false);
        }
    }
}