using ANSYS.Domain.Abstractions.Commands;
using ANSYS.Domain.Global.Pedidos;
using MediatR;

namespace ANSYS.Application.Global.Pedidos.Commands
{
    public sealed class PedidoCommandInsert : PedidoGeneral, ICommand, IRequest<int?>
    {
        public Guid UserId { get; set; }
    }
}
