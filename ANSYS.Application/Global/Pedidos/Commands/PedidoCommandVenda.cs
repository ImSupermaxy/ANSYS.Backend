using ANSYS.Application.Global.PedidoItens.Commands;
using ANSYS.Domain.Abstractions.Commands;
using ANSYS.Domain.Global.PedidoItens.Entities;
using ANSYS.Domain.Global.Pedidos;
using MediatR;

namespace ANSYS.Application.Global.Pedidos.Commands
{
    public class PedidoCommandVenda : ICommand, IRequest<int?>
    {
        public int ClienteId { get; set; }
        public List<PedidoItemCommandVenda> Itens { get; set; }

        public PedidoCommandInsert ToCommandInsert()
        {
            return new PedidoCommandInsert()
            {
                ClienteId = this.ClienteId,
                Subtotal = Itens.Sum(i => i.Subtotal * i.Quantidade),
                Taxa = Itens.Sum(i => i.Taxa * i.Quantidade),
                Desconto = Itens.Sum(i => i.Desconto * i.Quantidade),
            };
        }
    }
}
