using ANSYS.Domain.Global.Usuarios.Queries;
using MediatR;

namespace ANSYS.Application.Global.Usuarios.Commands
{
    public sealed record UsuarioCommandGetById(uint Id) : IRequest<UsuarioQueryResult>;
}
