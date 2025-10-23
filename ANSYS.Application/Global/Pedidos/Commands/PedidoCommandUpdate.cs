using ANSYS.Domain.Abstractions.Commands;
using ANSYS.Domain.Global.Pedidos;
using ANSYS.Domain.Global.Pedidos.Enums;
using MediatR;

namespace ANSYS.Application.Global.Pedidos.Commands
{
    public sealed class PedidoCommandUpdate : PedidoGeneral, ICommand, IRequest<bool>
    {
        public int Id { get; set; }
        public EStatusPedido Status { get; set; }

        public Guid UserId { get; set; }
    }
}
