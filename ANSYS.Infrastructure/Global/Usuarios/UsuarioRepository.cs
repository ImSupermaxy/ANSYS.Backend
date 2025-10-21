using ANSYS.Domain.Global.Usuarios.Entities;
using ANSYS.Domain.Global.Usuarios.Repositories;
using ANSYS.Infrastructure.Abstractions.Repositories;
using ANSYS.Infrastructure.Context.EntityFramework;

namespace ANSYS.Infrastructure.Global.Usuarios
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AnsysEntityFrameworkContext dbContext)
            : base(dbContext)
        {
        }
    }
}
