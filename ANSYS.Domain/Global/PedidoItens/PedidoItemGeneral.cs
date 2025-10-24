namespace ANSYS.Domain.Global.PedidoItens
{
    public class PedidoItemGeneral
    {
        public int PedidoId { get; set; }
        public int Quantidade { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Taxa { get; set; }
        public decimal Desconto { get; set; }
    }
}
