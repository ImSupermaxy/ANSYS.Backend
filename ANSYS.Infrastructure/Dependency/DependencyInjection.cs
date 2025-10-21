using ANSYS.Application.Global.Usuarios.Commands;
using ANSYS.Application.Global.Usuarios.Mappers;
using ANSYS.Domain.Abstractions.Context.EntityFramework;
using ANSYS.Domain.Global.Usuarios.Repositories;
using ANSYS.Infrastructure.Context.EntityFramework;
using ANSYS.Infrastructure.Global.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ANSYS.Dependency.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            AddPersistence(services, configuration);

            //AddAuthentication(services, configuration);

            return services;
        }

        private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
        {
            string cnnStringPostgres = configuration.GetConnectionString("Postgresql") ??
                                      throw new ArgumentNullException(nameof(configuration));

            string cnnStringMysql = configuration.GetConnectionString("Mysql") ??
                                      throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<AnsysEntityFrameworkContext>(options => options.UseNpgsql(cnnStringPostgres));
            //services.AddDbContext<AnsysEntityFrameworkContext>(options => options.UseMySQL(cnnStringMysql));

            //Entitys
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<UsuarioMapper, UsuarioMapper>();

            //Context
            services.AddScoped<IEntityFrameworkDBContext>(sp => sp.GetRequiredService<AnsysEntityFrameworkContext>());
        }

        //Configuração para gerar Token a partir do jwt bearer

        //private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
        //{
        //    //AppSettings
        //    services.AddSingleton<IAppAuthSettings>(configuration.GetSection("AuthSettings").Get<AppAuthSettings>()!);

        //    // Obtém a seção "AppSettings" do arquivo de configuração
        //    var appSettingsSection = configuration.GetSection("AuthSettings");

        //    //// Configura a classe AppSettings para ser injetada via DI
        //    services.Configure<AppAuthSettings>(appSettingsSection);

        //    // Obtém a instância de AppSettings
        //    var appSettings = (appSettingsSection.Get<AppAuthSettings>()!);

        //    // Converte a chave secreta para um array de bytes
        //    var key = Encoding.ASCII.GetBytes(appSettings.Segredo);

        //    // Configura o serviço de autenticação JWT
        //    services.AddAuthentication(options =>
        //    {
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //    .AddJwtBearer(options =>
        //    {
        //        options.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(key), // Usa a chave secreta do AppAuthSettings
        //            ValidateIssuer = false,
        //            ValidateAudience = false,
        //        };
        //    });
        //}
    }
}