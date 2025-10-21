using ANSYS.Domain.Global.Usuarios.Entities;
using MediatR;

namespace ANSYS.Application.Global.Usuarios.Commands
{
    public sealed class UsuarioCommandGetAll : IRequest<IEnumerable<Usuario>>
    {
    }
}
