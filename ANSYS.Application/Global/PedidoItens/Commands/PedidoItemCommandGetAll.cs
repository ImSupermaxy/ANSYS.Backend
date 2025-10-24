using ANSYS.Domain.Global.PedidoItens.Entities;
using MediatR;

namespace ANSYS.Application.Global.PedidoItens.Commands
{
    public sealed class PedidoItemCommandGetAll : IRequest<IEnumerable<PedidoItem>>
    {
    }
}
