using ANSYS.Domain.Abstractions.Commands;
using MediatR;

namespace ANSYS.Application.Global.PedidoItens.Commands
{
    public sealed record PedidoItemCommandInsertList(int PedidoId, List<PedidoItemCommandVenda> Itens) : ICommand, IRequest<List<int?>>
    {
    }
}
