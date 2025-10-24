using ANSYS.Domain.Abstractions.Commands;
using ANSYS.Domain.Global.Usuarios;
using MediatR;

namespace ANSYS.Application.Global.Usuarios.Commands
{
    public sealed class UsuarioCommandInsertFuncionario : UsuarioGeneral, ICommand, IRequest<Guid?>
    {
    }
}
