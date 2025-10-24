using ANSYS.Domain.Abstractions.Entities;
using ANSYS.Domain.Global.PedidoItens.Entities;
using ANSYS.Domain.Global.Pedidos.Enums;
using ANSYS.Domain.Global.Usuarios.Entities;

namespace ANSYS.Domain.Global.Pedidos.Entities
{
    public class Pedido : Entity
    {
        public int ClienteId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Taxa { get; set; }
        public decimal Desconto { get; set; }
        public decimal Total { get; set; }
        public EStatusPedido Status { get; set; }
        public TimeSpan DataInserido { get; set; }
        public TimeSpan DataModificado { get; set; }

        public Usuario Cliente { get; set; }

        public List<PedidoItem> Itens { get; set; }

        public Pedido(int clienteId, decimal subtotal, decimal taxa, decimal desconto)
        {
            ClienteId = clienteId;
            Subtotal = subtotal;
            Taxa = taxa;
            Desconto = desconto;
            Total = CalculaTotal();
            Status = EStatusPedido.PendentePagamento;

            CreateLog();
        }

        public Pedido(int id, int clienteId, decimal subtotal, decimal taxa, decimal desconto, 
            EStatusPedido status, TimeSpan dataInserido)
        {
            Id = id;
            ClienteId = clienteId;
            Subtotal = subtotal;
            Taxa = taxa;
            Desconto = desconto;
            Total = CalculaTotal();
            Status = status;

            //AtualizaLog(usuarioModId);
            AtualizaLog();
        }

        public Pedido(int id, int clienteId, decimal subtotal, decimal taxa, decimal desconto, decimal total,
            EStatusPedido status, TimeSpan dataInserido, TimeSpan dataModificado)
        {
            Id = id;
            ClienteId = clienteId;
            Subtotal = subtotal;
            Taxa = taxa;
            Desconto = desconto;
            Total = total;
            Status = status;
            DataInserido = dataInserido;
            DataModificado = dataModificado;
        }

        public void CancelaPedido(int usuarioModId)
        {
            this.Update(EStatusPedido.Cancelado, usuarioModId);
        }

        public void AprovaPedido(int usuarioModId)
        {
            this.Update(EStatusPedido.Pago, usuarioModId);
        }

        private decimal CalculaTotal()
        {
            return ((this.Subtotal + this.Taxa) - this.Desconto);
        }

        private void Update(EStatusPedido status, int usuarioModId)
        {
            Status = status;
            AtualizaLog(usuarioModId);
        }

        private void CreateLog(int? usuarioId = null)
        {
            var dataAgora = DateTime.Now.TimeOfDay;
            DataInserido = dataAgora;
            DataModificado = dataAgora;
        }

        private void AtualizaLog(int? usuarioId = null)
        {
            DataModificado = DateTime.Now.TimeOfDay;
        }
    }
}
