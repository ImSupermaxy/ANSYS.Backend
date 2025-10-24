using ANSYS.Application.Global.PedidoItens.Commands;
using ANSYS.Domain.Abstractions.Mappers;
using ANSYS.Domain.Global.PedidoItens.Entities;

namespace ANSYS.Application.Global.PedidoItens.Mappers
{
    public class PedidoItemMapper : IEntityMapper<PedidoItem, PedidoItemCommandInsert, PedidoItemCommandUpdate>
    {
        public PedidoItem ToEntity(PedidoItemCommandInsert command)
        {
            return new PedidoItem(command.PedidoId, command.Quantidade, command.Subtotal, command.Taxa, command.Desconto);
        }

        public PedidoItem ToEntity(PedidoItemCommandUpdate command, PedidoItem entity)
        {
            return new PedidoItem(command.Id, command.PedidoId, command.Quantidade, command.Subtotal, command.Taxa, command.Desconto);
        }

        public PedidoItem ToEntity(PedidoItemCommandVenda command, int pedidoId)
        {
            var commandInsert = new PedidoItemCommandInsert()
            {
                PedidoId = pedidoId,
                Quantidade = command.Quantidade,
                Desconto = command.Desconto,
                Taxa = command.Taxa,
                Subtotal = command.Subtotal,
            };

            return ToEntity(commandInsert);
        }
    }
}
