using ANSYS.Domain.Abstractions.Context;
using ANSYS.Domain.Global.Usuarios.Entities;
using ANSYS.Domain.Global.Usuarios.Queries;
using ANSYS.Domain.Global.Usuarios.Repositories;

namespace ANSYS.Infrastructure.Global.Usuarios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        //Deixar generico a ponto de poder utilizar o postgresql ou mysql
        private readonly IUnitOfWork _uow;

        public UsuarioRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<IEnumerable<UsuarioQueryResult>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioQueryResult> GetById(uint id)
        {
            throw new NotImplementedException();
        }

        public Task<uint> Insert(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(uint id)
        {
            throw new NotImplementedException();
        }
    }
}
