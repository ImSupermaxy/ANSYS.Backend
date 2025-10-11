using ANSYS.Domain.Abstractions.Repositories;
using ANSYS.Domain.Global.Usuarios.Entities;
using ANSYS.Domain.Global.Usuarios.Queries;

namespace ANSYS.Domain.Global.Usuarios.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario, UsuarioQueryResult>
    {

    }
}
