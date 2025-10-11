using ANSYS.Domain.Global.Usuarios.Queries;
using MediatR;

namespace ANSYS.Application.Global.Usuarios.Commands
{
    public sealed class UsuarioCommandGetAll : IRequest<IEnumerable<UsuarioQueryResult>>
    {
    }
}
