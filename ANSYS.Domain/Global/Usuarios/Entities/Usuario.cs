using ANSYS.Domain.Abstractions.Entities;

namespace ANSYS.Domain.Global.Usuarios.Entities
{
    public class Usuario : Entity
    {
        public string Nome { get; set; }
        public string Email { get; set; }

        //public string Senha { get; private set; }

        public Usuario (string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

        public Usuario(Guid id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
        }
    }
}
