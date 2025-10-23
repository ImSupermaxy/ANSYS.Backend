using ANSYS.Domain.Global.Pedidos.Entities;
using ANSYS.Domain.Global.Pedidos.Repositories;
using ANSYS.Infrastructure.Abstractions.Repositories;
using ANSYS.Infrastructure.Context.EntityFramework;

namespace ANSYS.Infrastructure.Global.Pedidos
{
    public class PedidoRepository : Repository<Pedido, int>, IPedidoRepository
    {
        public PedidoRepository(AnsysEntityFrameworkContext dbContext)
            : base(dbContext)
        {
        }
    }
}
