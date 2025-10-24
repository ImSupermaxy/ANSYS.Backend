using ANSYS.Application.Global.PedidoItens.Commands;
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
        private readonly ISender _sender;

        public PedidoHandler(IEntityFrameworkDBContext dbcotext, 
            IPedidoRepository repository,
            PedidoMapper mapper,
            ISender sender)
        {
            _dbcotext = dbcotext;
            _repository = repository;
            _mapper = mapper;
            _sender = sender;
        }

        public async Task<IEnumerable<Pedido>> Handle(PedidoCommandGetAll request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.GetAll(cancellationToken);

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
                return await _repository.GetById(request.Id, cancellationToken) ?? default!;
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

                var entity = _mapper.ToEntity(command);

                var result = await _repository.Insert(entity, cancellationToken);

                await _dbcotext.SaveChangesAsync();

                var lastInsert = (await _repository.GetAll(cancellationToken)).LastOrDefault();

                if (lastInsert != entity)
                    return null;

                var resultItens = await _sender.Send(new PedidoItemCommandInsertList(lastInsert.Id, request.Itens), cancellationToken);

                if (resultItens.Count() != request.Itens.Count())
                    return null;

                return lastInsert.Id;
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

                var entity = await _repository.GetById(request.Id, cancellationToken);
                if (entity == null)
                    return false;

                if (entity.Status == EStatusPedido.Cancelado)
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

        public async Task<bool> Handle(PedidoCommandCancelar request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _repository.GetById(request.Id, cancellationToken);
                if (entity == null)
                    return false;

                entity.CancelaPedido(UsuariosDefaultSystem.UserAtualizacao);

                var result = await _repository.Update(entity, cancellationToken);

                await _dbcotext.SaveChangesAsync(cancellationToken);

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
                var entity = await _repository.GetById(request.Id, cancellationToken);
                if (entity == null)
                    return false;

                entity.AprovaPedido(UsuariosDefaultSystem.UserAtualizacao);

                var result = await _repository.Update(entity);

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
