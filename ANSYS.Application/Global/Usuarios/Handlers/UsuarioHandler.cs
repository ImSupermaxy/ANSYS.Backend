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
        IRequestHandler<UsuarioCommandInsert, bool>,
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

        public async Task<bool> Handle(UsuarioCommandInsert request, CancellationToken cancellationToken)
        {
            try
            {
                var newId = await _repository.Insert(_mapper.ToEntity(request));

                await _dbcotext.SaveChangesAsync();

                return newId > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Handle(UsuarioCommandUpdate request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.Update(_mapper.ToEntity(request));

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
