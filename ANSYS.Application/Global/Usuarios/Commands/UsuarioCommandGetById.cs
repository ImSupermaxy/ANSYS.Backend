using ANSYS.Domain.Global.Usuarios.Entities;
using MediatR;

namespace ANSYS.Application.Global.Usuarios.Commands
{
    public sealed record UsuarioCommandGetById(uint Id) : IRequest<Usuario>;
}
