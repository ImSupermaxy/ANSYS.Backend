using ANSYS.Domain.Global.Pedidos.Enums;

namespace ANSYS.Domain.Global.Pedidos
{
    public class PedidoGeneral
    {
        public Guid ClienteId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Taxa { get; set; }
        public decimal Desconto { get; set; }
    }
}
