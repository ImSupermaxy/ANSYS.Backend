using ANSYS.Application.Global.Pedidos.Commands;
using ANSYS.Domain.Abstractions.Mappers;
using ANSYS.Domain.Global.Pedidos.Entities;

namespace ANSYS.Application.Global.Pedidos.Mappers
{
    public class PedidoMapper : IEntityMapper<Pedido, PedidoCommandInsert, PedidoCommandUpdate>
    {
        public Pedido ToEntity(PedidoCommandInsert command)
        {
            return new Pedido(command.ClienteId, command.Subtotal, command.Taxa, command.Desconto);
        }

        public Pedido ToEntity(PedidoCommandUpdate command, Pedido entity)
        {
            return new Pedido(command.Id, command.ClienteId, command.Subtotal, command.Taxa, command.Desconto, 
                command.Status, entity.DataInserido);
        }
    }
}
