using ANSYS.Domain.Global.PedidoItens.Entities;
using MediatR;

namespace ANSYS.Application.Global.PedidoItens.Commands
{
    public sealed record PedidoItemCommandGetById(int Id) : IRequest<PedidoItem>
    {
    }
}
