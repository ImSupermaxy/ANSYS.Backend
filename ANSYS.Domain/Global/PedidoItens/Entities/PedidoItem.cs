using ANSYS.Domain.Abstractions.Entities;
using ANSYS.Domain.Global.Pedidos.Entities;

namespace ANSYS.Domain.Global.PedidoItens.Entities
{
    public class PedidoItem : Entity
    {
        public int PedidoId { get; set; }
        public int Quantidade { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Taxa { get; set; }
        public decimal Desconto { get; set; }

        public Pedido Pedido { get; set; }

        public PedidoItem(int pedidoId, int quantidade, decimal subtotal, decimal taxa, decimal desconto)
        {
            PedidoId = pedidoId;
            Quantidade = quantidade;
            Subtotal = subtotal;
            Taxa = taxa;
            Desconto = desconto;
        }

        public PedidoItem(int id, int pedidoId, int quantidade, decimal subtotal, decimal taxa, decimal desconto)
        {
            Id = id;
            PedidoId = pedidoId;
            Quantidade = quantidade;
            Subtotal = subtotal;
            Taxa = taxa;
            Desconto = desconto;
        }

        public PedidoItem(int id, int pedidoId, int quantidade, decimal subtotal, decimal taxa, decimal desconto, decimal total)
        {
            Id = id;
            PedidoId = pedidoId;
            Quantidade = quantidade;
            Subtotal = subtotal;
            Taxa = taxa;
            Desconto = desconto;
        }
    }
}
