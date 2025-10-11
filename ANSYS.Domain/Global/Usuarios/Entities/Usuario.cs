using ANSYS.Domain.Abstractions.Entities;

namespace ANSYS.Domain.Global.Usuarios.Entities
{
    public class Usuario : IEntity
    {
        public uint Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }

        //public string Senha { get; private set; }

        public Usuario (string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

        public Usuario(uint id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
        }
    }
}
