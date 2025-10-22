using ANSYS.Domain.Abstractions.Commands;
using ANSYS.Domain.Global.Usuarios;
using MediatR;

namespace ANSYS.Application.Global.Usuarios.Commands
{
    public sealed class UsuarioCommandUpdate : UsuarioGeneral, ICommand, IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
