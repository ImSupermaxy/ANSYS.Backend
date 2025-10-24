using ANSYS.Domain.Global.Usuarios.Entities;
using ANSYS.Domain.Global.Usuarios.Repositories;
using ANSYS.Infrastructure.Abstractions.Repositories;
using ANSYS.Infrastructure.Context;
using ANSYS.Infrastructure.Context.EntityFramework;

namespace ANSYS.Infrastructure.Global.Usuarios
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AnsysEntityFrameworkContext dbContext)
            : base(dbContext)
        {
            if (DatabaseRunModeConfiguration.IsLocalDataBase && !DatabaseRunModeConfiguration.IsInitializateUsuario)
            {
                var usuarios = new List<Usuario>()
                {
                    new Usuario(1, "Master", "master@ansys.com", Domain.Global.Usuarios.Enums.EPerfilUsuario.Administrador),
                    new Usuario(2, "Administrador", "admin@ansys.com", Domain.Global.Usuarios.Enums.EPerfilUsuario.Administrador)
                };

                foreach (var item in usuarios)
                {
                    var task = base.Insert(item);
                    task.Wait();
                }

                DatabaseRunModeConfiguration.InitializatedUsuario();
            }
        }
    }
}
