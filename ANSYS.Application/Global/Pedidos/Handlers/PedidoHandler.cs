using ANSYS.Application.Global.Pedidos.Commands;
using ANSYS.Application.Global.Pedidos.Mappers;
using ANSYS.Application.Utils.Constants;
using ANSYS.Domain.Abstractions.Context.EntityFramework;
using ANSYS.Domain.Global.Pedidos.Entities;
using ANSYS.Domain.Global.Pedidos.Enums;
using ANSYS.Domain.Global.Pedidos.Repositories;
using MediatR;

namespace ANSYS.Application.Global.Pedidos.Handlers
{
    public class PedidoHandler : IRequestHandler<PedidoCommandGetAll, IEnumerable<Pedido>>,
        IRequestHandler<PedidoCommandGetById, Pedido>,
        IRequestHandler<PedidoCommandVenda, int?>,
        IRequestHandler<PedidoCommandUpdate, bool>,
        IRequestHandler<PedidoCommandCancelar, bool>,
        IRequestHandler<PedidoCommandAprovar, bool>
    {
        private readonly IEntityFrameworkDBContext _dbcotext;
        private readonly IPedidoRepository _repository;
        private readonly PedidoMapper _mapper;

        public PedidoHandler(IEntityFrameworkDBContext dbcotext, IPedidoRepository repository, PedidoMapper mapper)
        {
            _dbcotext = dbcotext;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Pedido>> Handle(PedidoCommandGetAll request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.GetAll();

                //fazer o filtro aqui?...
                return result;
            }
            catch (Exception ex)
            {
                return default!;
            }
        }

        public async Task<Pedido> Handle(PedidoCommandGetById request, CancellationToken cancellationToken)
        {
            try
            {
                return await _repository.GetById(request.Id) ?? default!;
            }
            catch (Exception ex)
            {
                return default!;
            }
        }

        public async Task<int?> Handle(PedidoCommandVenda request, CancellationToken cancellationToken)
        {
            try
            {
                var command = request.ToCommandInsert();
                command.UserId = UsuariosDefaultSystem.UserDefault;

                var result = await _repository.Insert(_mapper.ToEntity(command));
                //if (!(result > 0))
                //    return 0;

                await _dbcotext.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Handle(PedidoCommandUpdate request, CancellationToken cancellationToken)
        {
            try
            {
                request.UserId = UsuariosDefaultSystem.UserAtualizacao;

                var entity = await _repository.GetById(request.Id);
                if (entity == null)
                    return false;

                if (entity.Status == EStatusPedido.Cancelado)
                    return false;

                var result = await _repository.Update(_mapper.ToEntity(request, entity));

                await _dbcotext.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Handle(PedidoCommandCancelar request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _repository.GetById(request.Id);
                if (entity == null)
                    return false;

                entity.CancelaPedido(UsuariosDefaultSystem.UserAtualizacao);

                var result = await _repository.Update(entity);

                await _dbcotext.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Handle(PedidoCommandAprovar request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _repository.GetById(request.Id);
                if (entity == null)
                    return false;

                entity.AprovaPedido(UsuariosDefaultSystem.UserAtualizacao);

                var result = await _repository.Update(entity);

                await _dbcotext.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private async Task<int> GetLastId()
        {
            var lastPedido = (await _repository.GetAll()).LastOrDefault();
            var lastId = 0;
            if (lastPedido != null)
                lastId = lastPedido.Id;

            return lastId;
        }
    }
}
