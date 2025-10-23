using ANSYS.Application.Global.Usuarios.Commands;
using ANSYS.Application.Global.Usuarios.Mappers;
using ANSYS.Domain.Abstractions.Context.EntityFramework;
using ANSYS.Domain.Global.Usuarios.Entities;
using ANSYS.Domain.Global.Usuarios.Repositories;
using MediatR;

namespace ANSYS.Application.Global.Usuarios.Handlers
{
    public class UsuarioHandler : IRequestHandler<UsuarioCommandGetAll, IEnumerable<Usuario>>,
        IRequestHandler<UsuarioCommandGetById, Usuario>,
        IRequestHandler<UsuarioCommandInsert, Guid?>,
        IRequestHandler<UsuarioCommandUpdate, bool>
    {
        private readonly IEntityFrameworkDBContext _dbcotext;
        private readonly IUsuarioRepository _repository;
        private readonly UsuarioMapper _mapper;

        public UsuarioHandler(IEntityFrameworkDBContext dbcotext, IUsuarioRepository repository, UsuarioMapper mapper)
        {
            _dbcotext = dbcotext;
            _repository = repository;
            _mapper = mapper;
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

        public async Task<Guid?> Handle(UsuarioCommandInsert request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.Insert(_mapper.ToEntity(request));
                if (result != null)
                    return result;

                await _dbcotext.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Handle(UsuarioCommandUpdate request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _repository.GetById(request.Id);
                if (entity == null)
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
    }
}
