using ANSYS.Domain.Abstractions.Entities;
using ANSYS.Domain.Global.Pedidos.Entities;
using ANSYS.Domain.Global.Usuarios.Enums;

namespace ANSYS.Domain.Global.Usuarios.Entities
{
    public class Usuario : Entity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public EPerfilUsuario Perfil { get; set; }

        public readonly List<Pedido> Pedidos = Enumerable.Empty<Pedido>().ToList();

        public Usuario(string nome, string email)
        {
            Nome = nome;
            Email = email;
            Perfil = EPerfilUsuario.Cliente;
        }

        public Usuario(string nome, string email, EPerfilUsuario perfil)
        {
            Nome = nome;
            Email = email;
            Perfil = perfil;
        }

        public Usuario(int id, string nome, string email, EPerfilUsuario perfil)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Perfil = perfil;
        }

        public void Update(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }
    }
}
