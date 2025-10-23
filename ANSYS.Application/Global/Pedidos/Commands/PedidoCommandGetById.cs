using ANSYS.Domain.Global.Pedidos.Entities;
using MediatR;

namespace ANSYS.Application.Global.Pedidos.Commands
{
    public sealed record PedidoCommandGetById(int Id) : IRequest<Pedido>;
}
