using ANSYS.Domain.Abstractions.Context;
using ANSYS.Domain.Global.Usuarios.Entities;
using ANSYS.Domain.Global.Usuarios.Queries;
using ANSYS.Domain.Global.Usuarios.Repositories;
using Dapper;
using System.Data;
using System.Text;

namespace ANSYS.Infrastructure.Global.Usuarios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        //Deixar generico a ponto de poder utilizar o postgresql ou mysql
        private readonly IUnitOfWork _uow;
        private readonly string _tableName = "usuario";

        public UsuarioRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<UsuarioQueryResult>> GetAll()
        {
            var query = new StringBuilder($"SELECT * FROM {_tableName};");

            return await _uow.Context.Connection!
                .QueryAsync<UsuarioQueryResult>(query.ToString(), commandType: CommandType.Text);
        }

        public async Task<UsuarioQueryResult> GetById(uint id)
        {
            var query = new StringBuilder($"SELECT * FROM {_tableName} WHERE id = @Id;");

            return await _uow.Context.Connection!
                .QueryFirstOrDefaultAsync<UsuarioQueryResult>(query.ToString(),
                    new { 
                        Id = id 
                    }, 
                    commandType: CommandType.Text) ?? default!;
        }

        public async Task<uint> Insert(Usuario entity)
        {
            var query = new StringBuilder($"INSERT INTO {_tableName} (nome, email) VALUES (@Nome, @Email); SELECT MAX(id) from {_tableName};");

            return await _uow.Context.Connection!
                .ExecuteScalarAsync<uint>(query.ToString(),
                    new
                    {
                        Nome = entity.Nome,
                        Email = entity.Email,
                    },
                    commandType: CommandType.Text);
        }

        public Task<bool> Update(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(uint id)
        {
            var query = new StringBuilder($"DELETE FROM {_tableName} WHERE id = @Id;");

            await _uow.Context.Connection!
                .ExecuteAsync(query.ToString(),
                    new
                    {
                        Id = id
                    },
                    commandType: CommandType.Text);

            return true;
        }
    }
}
