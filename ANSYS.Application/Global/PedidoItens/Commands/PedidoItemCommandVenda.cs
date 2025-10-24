using ANSYS.Domain.Abstractions.Commands;
using MediatR;

namespace ANSYS.Application.Global.PedidoItens.Commands
{
    public sealed class PedidoItemCommandVenda : ICommand, IRequest<int?>
    {
        public int Quantidade { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Taxa { get; set; }
        public decimal Desconto { get; set; }
    }
}
