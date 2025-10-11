using ANSYS.Application.Global.Usuarios.Commands;
using ANSYS.Domain.Abstractions.Mappers;
using ANSYS.Domain.Global.Usuarios.Entities;
using ANSYS.Domain.Global.Usuarios.Queries;

namespace ANSYS.Application.Global.Usuarios.Mappers
{
    public class UsuarioMapper : IEntityMapper<Usuario, UsuarioCommandInsert, UsuarioCommandUpdate, UsuarioQueryResult>
    {
        public Usuario ToEntity(UsuarioCommandInsert command)
        {
            return new Usuario(command.Nome, command.Email);
        }

        public Usuario ToEntity(UsuarioCommandUpdate command)
        {
            return new Usuario(command.Id, command.Nome, command.Email);
        }

        public Usuario ToEntity(UsuarioQueryResult command)
        {
            return new Usuario(command.Id, command.Nome, command.Email);
        }

        public UsuarioQueryResult ToQuery(Usuario entity)
        {
            return new UsuarioQueryResult { Id = entity.Id, Nome = entity.Nome, Email = entity.Email };
        }
    }
}
