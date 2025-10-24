using ANSYS.Domain.Global.PedidoItens.Entities;
using ANSYS.Domain.Global.PedidoItens.Repositories;
using ANSYS.Infrastructure.Abstractions.Repositories;
using ANSYS.Infrastructure.Context.EntityFramework;

namespace ANSYS.Infrastructure.Global.PedidoItens
{
    public class PedidoItemRepository : Repository<PedidoItem>, IPedidoItemRepository
    {
        public PedidoItemRepository(AnsysEntityFrameworkContext dbContext)
            : base(dbContext)
        {
        }
    }
}
