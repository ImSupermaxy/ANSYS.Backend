using ANSYS.Domain.Abstractions.Entities;
using ANSYS.Domain.Global.Pedidos.Enums;
using ANSYS.Domain.Global.Usuarios.Entities;

namespace ANSYS.Domain.Global.Pedidos.Entities
{
    public class Pedido : Entity<int>
    {
        public Guid ClienteId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Taxa { get; set; }
        public decimal Desconto { get; set; }
        public decimal Total { get; set; }
        public EStatusPedido Status { get; set; }
        public TimeSpan DataInserido { get; set; }
        //public Guid UsuarioInsId { get; set; }
        public TimeSpan DataModificado { get; set; }
        //public Guid UsuarioModId { get; set; }

        public Usuario Cliente { get; set; }
        //public Usuario UsuarioInseriu { get; set; }
        //public Usuario UsuarioModificou { get; set; }


        public Pedido(Guid clienteId, decimal subtotal, decimal taxa, decimal desconto/*, Guid usuarioId*/)
        {
            ClienteId = clienteId;
            Subtotal = subtotal;
            Taxa = taxa;
            Desconto = desconto;
            Total = CalculaTotal();
            Status = EStatusPedido.PendentePagamento;

            //CreateLog(usuarioId);
            CreateLog();
        }

        public Pedido(int id, Guid clienteId, decimal subtotal, decimal taxa, decimal desconto, 
            EStatusPedido status, /*Guid usuarioInsId, */TimeSpan dataInserido/*, Guid usuarioModId*/)
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

        public Pedido(int id, Guid clienteId, decimal subtotal, decimal taxa, decimal desconto, decimal total,
            EStatusPedido status, /*Guid usuarioInsId, */TimeSpan dataInserido, /*Guid usuarioModId,*/ TimeSpan dataModificado)
        {
            Id = id;
            ClienteId = clienteId;
            Subtotal = subtotal;
            Taxa = taxa;
            Desconto = desconto;
            Total = total;
            Status = status;
            //UsuarioInsId = usuarioInsId;
            //UsuarioModId = usuarioModId;
            DataInserido = dataInserido;
            DataModificado = dataModificado;
        }

        public void CancelaPedido(Guid usuarioModId)
        {
            this.Update(EStatusPedido.Cancelado, usuarioModId);
        }

        public void AprovaPedido(Guid usuarioModId)
        {
            this.Update(EStatusPedido.Pago, usuarioModId);
        }

        private decimal CalculaTotal()
        {
            return ((this.Subtotal + this.Taxa) - this.Desconto);
        }

        private void Update(EStatusPedido status, Guid usuarioModId)
        {
            Status = status;
            AtualizaLog(usuarioModId);
        }

        private void CreateLog(Guid? usuarioId = null)
        {
            var dataAgora = DateTime.Now.TimeOfDay;
            //UsuarioInsId = usuarioId;
            DataInserido = dataAgora;
            //UsuarioModId = UsuarioModId;
            DataModificado = dataAgora;
        }

        private void AtualizaLog(Guid? usuarioId = null)
        {
            //UsuarioModId = usuarioId;
            DataModificado = DateTime.Now.TimeOfDay;
        }
    }
}
