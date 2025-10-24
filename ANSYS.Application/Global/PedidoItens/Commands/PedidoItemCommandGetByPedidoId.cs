using ANSYS.Domain.Global.PedidoItens.Entities;
using MediatR;

namespace ANSYS.Application.Global.PedidoItens.Commands
{
    public sealed record PedidoItemCommandGetByPedidoId(int PedidoId) : IRequest<IEnumerable<PedidoItem>>
    {
    }
}
