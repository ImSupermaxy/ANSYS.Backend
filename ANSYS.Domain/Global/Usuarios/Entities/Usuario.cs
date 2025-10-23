using ANSYS.Domain.Abstractions.Entities;
using ANSYS.Domain.Global.Pedidos.Entities;

namespace ANSYS.Domain.Global.Usuarios.Entities
{
    public class Usuario : Entity<Guid>
    {
        public string Nome { get; set; }
        public string Email { get; set; }

        //public string Senha { get; set; }

        public readonly List<Pedido> Pedidos = Enumerable.Empty<Pedido>().ToList();
        public readonly List<Pedido> PedidosInseridos = Enumerable.Empty<Pedido>().ToList();
        public readonly List<Pedido> PedidosAtualizados = Enumerable.Empty<Pedido>().ToList();

        //Construtor de Insert na base
        public Usuario(string nome, string email)
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

        //Construtor de Update / entidades que precisam de todas asinfos do usuário

        public void Update(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }
    }
}
