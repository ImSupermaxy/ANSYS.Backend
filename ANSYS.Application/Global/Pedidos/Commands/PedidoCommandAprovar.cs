using MediatR;

namespace ANSYS.Application.Global.Pedidos.Commands
{
    public sealed record PedidoCommandAprovar(int Id) : IRequest<bool>
    {
    }
}
