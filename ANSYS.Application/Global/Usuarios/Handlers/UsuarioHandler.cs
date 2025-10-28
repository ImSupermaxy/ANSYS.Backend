using ANSYS.Application.Global.Usuarios.Commands;
using ANSYS.Application.Global.Usuarios.Mappers;
using ANSYS.Domain.Abstractions.Context.EntityFramework;
using ANSYS.Domain.Abstractions.Entities;
using ANSYS.Domain.Abstractions.Mappers;
using ANSYS.Domain.Global.Usuarios.Entities;
using ANSYS.Domain.Global.Usuarios.Repositories;
using MediatR;

namespace ANSYS.Application.Global.Usuarios.Handlers
{
    public class UsuarioHandler : IRequestHandler<UsuarioCommandGetAll, IEnumerable<Usuario>>,
        IRequestHandler<UsuarioCommandGetById, Usuario>,
        IRequestHandler<UsuarioCommandInsert, int?>,
        IRequestHandler<UsuarioCommandUpdate, bool?>
    {
        private readonly IEntityFrameworkDBContext _dbcotext;
        private readonly IUsuarioRepository _repository;
        private readonly IEntityMapper<Usuario, UsuarioCommandInsert, UsuarioCommandUpdate> _mapper;
        private readonly ISender _sender;

        public UsuarioHandler(IEntityFrameworkDBContext dbcotext, 
            IUsuarioRepository repository,
            IEntityMapper<Usuario, UsuarioCommandInsert, UsuarioCommandUpdate> mapper, 
            ISender sender)
        {
            _dbcotext = dbcotext;
            _repository = repository;
            _mapper = mapper;
            _sender = sender;
        }

        public async Task<IEnumerable<Usuario>> Handle(UsuarioCommandGetAll request, CancellationToken cancellationToken)
        {
            try
            {
                return await _repository.GetAll();
            }
            catch (Exception ex)
            {
                return default!;
            }
        }

        public async Task<Usuario> Handle(UsuarioCommandGetById request, CancellationToken cancellationToken)
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

        public async Task<int?> Handle(UsuarioCommandInsert request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.ToEntity(request);

                var usuarios = await _sender.Send(new UsuarioCommandGetAll());
                if (usuarios.Any(u => u.Email == request.Email))
                    return null;

                var result = await _repository.Insert(entity);

                await _dbcotext.SaveChangesAsync();

                var lastInsert = (await _repository.GetAll(cancellationToken)).LastOrDefault();

                if (lastInsert != entity)
                    return null;

                return lastInsert.Id;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool?> Handle(UsuarioCommandUpdate request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _repository.GetById(request.Id);
                if (entity == null)
                    return false;

                var usuarios = await _sender.Send(new UsuarioCommandGetAll());
                var updatedEntity = _mapper.ToEntity(request, entity);

                if (usuarios.Any(u => u.Email == updatedEntity.Email))
                    return null;

                var result = await _repository.Update(updatedEntity);

                await _dbcotext.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
