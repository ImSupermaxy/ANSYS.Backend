using ANSYS.Domain.Global.Pedidos.Entities;
using ANSYS.Domain.Global.Pedidos.Enums;
using MediatR;

namespace ANSYS.Application.Global.Pedidos.Commands
{
    public sealed record PedidoCommandGetAll(string cliente, int? numeroPedido, EStatusPedido status) : IRequest<IEnumerable<Pedido>>
    {
    }
}
