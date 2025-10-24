using ANSYS.Application.Global.PedidoItens.Commands;
using ANSYS.Application.Global.PedidoItens.Mappers;
using ANSYS.Domain.Abstractions.Context.EntityFramework;
using ANSYS.Domain.Global.PedidoItens.Entities;
using ANSYS.Domain.Global.PedidoItens.Repositories;
using MediatR;

namespace ANSYS.Application.Global.PedidoItens.Handlers
{
    public class PedidoItemHandler : IRequestHandler<PedidoItemCommandGetAll, IEnumerable<PedidoItem>>,
        IRequestHandler<PedidoItemCommandGetByPedidoId, IEnumerable<PedidoItem>>,
        IRequestHandler<PedidoItemCommandGetById, PedidoItem>,
        IRequestHandler<PedidoItemCommandInsertList, List<int?>>,
        IRequestHandler<PedidoItemCommandUpdate, bool>
    {
        private readonly IEntityFrameworkDBContext _dbcotext;
        private readonly IPedidoItemRepository _repository;
        private readonly PedidoItemMapper _mapper;

        public PedidoItemHandler(IEntityFrameworkDBContext dbcotext, IPedidoItemRepository repository, PedidoItemMapper mapper)
        {
            _dbcotext = dbcotext;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PedidoItem>> Handle(PedidoItemCommandGetAll request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.GetAll(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                return default!;
            }
        }

        public async Task<IEnumerable<PedidoItem>> Handle(PedidoItemCommandGetByPedidoId request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.GetAll(cancellationToken);

                result = result.Where(r => r.PedidoId == request.PedidoId);

                return result;
            }
            catch (Exception ex)
            {
                return default!;
            }
        }

        public async Task<PedidoItem> Handle(PedidoItemCommandGetById request, CancellationToken cancellationToken)
        {
            try
            {
                return await _repository.GetById(request.Id, cancellationToken) ?? default!;
            }
            catch (Exception ex)
            {
                return default!;
            }
        }

        public async Task<List<int?>> Handle(PedidoItemCommandInsertList request, CancellationToken cancellationToken)
        {
            try
            {
                var results = new List<int?>();
                foreach (var item in request.Itens)
                {
                    var entity = _mapper.ToEntity(item!, request.PedidoId);

                    var result = await _repository.Insert(entity, cancellationToken);

                    await _dbcotext.SaveChangesAsync(cancellationToken);

                    var entitys = await _repository.GetAll(cancellationToken);
                    var lastInsert = (entitys).LastOrDefault();

                    if (lastInsert != entity)
                        return new List<int?>();

                    results.Add(lastInsert.Id);
                }

                return results;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Handle(PedidoItemCommandUpdate request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _repository.GetById(request.Id, cancellationToken);
                if (entity == null)
                    return false;

                var result = await _repository.Update(_mapper.ToEntity(request, entity), cancellationToken);

                await _dbcotext.SaveChangesAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
