using ANSYS.Domain.Abstractions.Commands;
using ANSYS.Domain.Global.PedidoItens;
using MediatR;

namespace ANSYS.Application.Global.PedidoItens.Commands
{
    public sealed class PedidoItemCommandUpdate : PedidoItemGeneral, ICommand, IRequest<bool>
    {
        public int Id { get; set; }
    }
}
