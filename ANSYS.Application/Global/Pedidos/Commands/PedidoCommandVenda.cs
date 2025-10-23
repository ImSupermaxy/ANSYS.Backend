using ANSYS.Domain.Abstractions.Commands;
using ANSYS.Domain.Global.Pedidos;
using MediatR;

namespace ANSYS.Application.Global.Pedidos.Commands
{
    public class PedidoCommandVenda : PedidoGeneral, ICommand, IRequest<int?>
    {
        public PedidoCommandInsert ToCommandInsert()
        {
            return new PedidoCommandInsert()
            {
                ClienteId = this.ClienteId,
                Subtotal = this.Subtotal,
                Taxa = this.Taxa,
                Desconto = this.Desconto,
            };
        }


    }
}
