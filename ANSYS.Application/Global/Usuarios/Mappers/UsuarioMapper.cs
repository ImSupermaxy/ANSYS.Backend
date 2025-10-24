using ANSYS.Application.Global.Usuarios.Commands;
using ANSYS.Domain.Abstractions.Mappers;
using ANSYS.Domain.Global.Usuarios.Entities;
using ANSYS.Domain.Global.Usuarios.Enums;

namespace ANSYS.Application.Global.Usuarios.Mappers
{
    public class UsuarioMapper : IEntityMapper<Usuario, UsuarioCommandInsert, UsuarioCommandUpdate>
    {
        public Usuario ToEntity(UsuarioCommandInsert command)
        {
            return new Usuario(command.Nome, command.Email);
        }

        public Usuario ToEntity(UsuarioCommandUpdate command, Usuario entity)
        {
            entity.Update(command.Nome, command.Email);
            return entity;
        }

        public Usuario ToEntity(UsuarioCommandInsertFuncionario command)
        {
            return new Usuario(command.Nome, command.Email, EPerfilUsuario.Funcionario);
        }
    }
}
