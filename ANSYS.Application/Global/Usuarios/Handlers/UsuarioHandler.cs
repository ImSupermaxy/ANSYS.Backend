using ANSYS.Application.Global.Usuarios.Commands;
using ANSYS.Application.Global.Usuarios.Mappers;
using ANSYS.Domain.Abstractions.Mappers;
using ANSYS.Domain.Global.Usuarios.Queries;
using ANSYS.Domain.Global.Usuarios.Repositories;
using MediatR;

namespace ANSYS.Application.Global.Usuarios.Handlers
{
    public class UsuarioHandler : IRequestHandler<UsuarioCommandGetAll, IEnumerable<UsuarioQueryResult>>,
        IRequestHandler<UsuarioCommandGetById, UsuarioQueryResult>,
        IRequestHandler<UsuarioCommandInsert, bool>,
        IRequestHandler<UsuarioCommandUpdate, bool>
    {
        private readonly IUsuarioRepository _repository;
        private readonly UsuarioMapper _mapper;

        public UsuarioHandler(IUsuarioRepository repository, UsuarioMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioQueryResult>> Handle(UsuarioCommandGetAll request, CancellationToken cancellationToken)
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

        public async Task<UsuarioQueryResult> Handle(UsuarioCommandGetById request, CancellationToken cancellationToken)
        {
            try
            {
                return await _repository.GetById(request.Id);

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
                return await _repository.Update(_mapper.ToEntity(request));
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
