using MediatR;

namespace ANSYS.Application.Global.Pedidos.Commands
{
    public sealed record PedidoCommandCancelar(int Id): IRequest<bool>
    {
    }
}
