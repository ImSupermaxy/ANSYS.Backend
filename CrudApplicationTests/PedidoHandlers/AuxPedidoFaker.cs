using ANSYS.Domain.Global.Pedidos.Entities;
using ANSYS.Domain.Global.Pedidos.Enums;

namespace CrudApplicationTests.PedidoHandlers
{
    public static class AuxPedidoFaker
    {
        //Methods Aux..
        public static List<Pedido> ObterFakesToMock()
        {
            var userMaster = 1;
            var userAdmin = 2;

            var pedidosFake = new List<Pedido>
            {
                new Pedido(1, userMaster, 100.0M, 50.0M, 10.0M, 140.0M, EStatusPedido.PendentePagamento, DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay),
                new Pedido(2, userMaster, 200.5M, 10.0M, 0.0M, 210.5M, EStatusPedido.Cancelado, DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay),
                new Pedido(3, userAdmin, 83.0M, 17.5M, 5.0M, 95.5M, EStatusPedido.Pago, DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay),
                new Pedido(4, userAdmin, 42.0M, 2.3M, 1.3M, 43.0M, EStatusPedido.PendentePagamento, DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay),
                new Pedido(5, userMaster, 327.0M, 3.0M, 0.0M, 330.0M, EStatusPedido.Cancelado, DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay),
                new Pedido(6, userAdmin, 380.2M, 50.5M, 30.7M, 400.0M, EStatusPedido.Cancelado, DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay),
                new Pedido(7, userMaster, 250.0M, 20.0M, 10.0M, 260.0M, EStatusPedido.Pago, DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay),
            };

            return pedidosFake;
        }

        public static List<Pedido> ObterFakesToMock(List<Pedido> pedidos)
        {
            var pedidosFake = ObterFakesToMock();

            return pedidos.Concat(pedidosFake).ToList();
        }

        public static List<Pedido> ObterFakesToMock(Pedido pedido)
        {
            var pedidosFake = ObterFakesToMock();
            pedidosFake.Add(pedido);

            return pedidosFake;
        }

        public static Pedido ObterFakeToMock()
        {
            var pedidossFake = ObterFakesToMock();

            var random = new Random();
            var idx = random.Next(pedidossFake.Count() - 1);

            return pedidossFake[idx];
        }

        public static Pedido ObterFakeToMock(bool useDefault = true)
        {
            if (useDefault)
                return ObterFakeToMock();

            var newId = ObterFakesToMock().Count() + 1;
            var pedido = new Pedido(newId, 3, 1330.0M, 100.0M, 50.0M, 1380.0M, EStatusPedido.PendentePagamento, DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay);

            return pedido;
        }

        public static Pedido ObterFakeToMock(Pedido pedido = default!, int id = default, int clienteId = default!, decimal total = default!, decimal subtotal = default!, decimal taxa = default!, decimal desconto = default!, EStatusPedido? status = default!)
        {
            var pedidoFake = ObterFakeToMock();
            if (pedido != default)
                pedidoFake = pedido;

            if (id == default)
            {
                id = pedidoFake.Id;
            }

            if (clienteId == default)
            {
                total = pedidoFake.ClienteId;
            }

            if (total == default)
            {
                total = pedidoFake.Total;
            }

            if (subtotal == default)
            {
                subtotal = pedidoFake.Subtotal;
            }

            if (taxa == default)
            {
                taxa = pedidoFake.Taxa;
            }

            if (desconto == default)
            {
                desconto = pedidoFake.Desconto;
            }

            if (status == default)
            {
                status = pedidoFake.Status;
            }

            pedidoFake = new Pedido(id, clienteId, total, subtotal, taxa, desconto, status!.Value, DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay);

            return pedidoFake;
        }
    }
}
