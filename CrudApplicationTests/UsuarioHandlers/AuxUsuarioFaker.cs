using ANSYS.Domain.Global.Usuarios.Entities;
using ANSYS.Domain.Global.Usuarios.Enums;

namespace CrudApplicationTests.UsuarioHandlers
{
    public static class AuxUsuarioFaker
    {
        //Methods Aux..
        public static List<Usuario> ObterUsuariosFakeToMock()
        {
            var usuariosFake = new List<Usuario>
            {
                new Usuario(1, "Master", "master@ansys.com", EPerfilUsuario.Administrador),
                new Usuario(2, "Administrador", "admin@ansys.com", EPerfilUsuario.Administrador),
                new Usuario(3, "Matheus", "matheus@ansys.com", EPerfilUsuario.Funcionario),
                new Usuario(4, "Isabel", "isabel@ansys.com", EPerfilUsuario.Administrador),
                new Usuario(5, "Gustavo", "gustavo@ansys.com", EPerfilUsuario.Cliente),
                new Usuario(6, "Guilherme", "guilherme@ansys.com", EPerfilUsuario.Cliente),
                new Usuario(7, "Lucas", "lucas@ansys.com", EPerfilUsuario.Funcionario),
                new Usuario(8, "Lucas2", "lucas2@ansys.com", EPerfilUsuario.Cliente),
                new Usuario(9, "OtherUser", "otheruser@ansys.com", EPerfilUsuario.Cliente)
            };

            return usuariosFake;
        }

        public static List<Usuario> ObterUsuariosFakeToMock(List<Usuario> usuarios)
        {
            var usuariosFake = ObterUsuariosFakeToMock();

            return usuarios.Concat(usuariosFake).ToList();
        }

        public static List<Usuario> ObterUsuariosFakeToMock(Usuario usuario)
        {
            var usuariosFake = ObterUsuariosFakeToMock();
            usuariosFake.Add(usuario);

            return usuariosFake;
        }

        public static Usuario ObterUsuarioFakeToMock()
        {
            var usuariosFake = ObterUsuariosFakeToMock();

            var random = new Random();
            var idx = random.Next(usuariosFake.Count() - 1);

            return usuariosFake[idx];
        }

        public static Usuario ObterUsuarioFakeToMock(int? idx = null)
        {
            var usuariosFake = ObterUsuariosFakeToMock();
            if (idx == null || idx < 0 || idx > usuariosFake.Count() - 1)
                return ObterUsuarioFakeToMock();

            return usuariosFake[idx.Value];
        }

        public static Usuario ObterUsuarioFakeToMock(bool useDefault = true)
        {
            if (useDefault)
                return ObterUsuarioFakeToMock();

            var newId = ObterUsuariosFakeToMock().Count() + 1;
            var usuario = new Usuario(
                newId,
                "Usuario não inserido",
                "usernotindatabase@ansys.com",
                EPerfilUsuario.Cliente);

            return usuario;
        }

        public static Usuario ObterUsuarioFakeToMock(Usuario usuario = default!, int id = default, string nome = default!, string email = default!, EPerfilUsuario perfil = EPerfilUsuario.Cliente)
        {
            var usuarioFake = ObterUsuarioFakeToMock();
            if (usuario != default)
                usuarioFake = usuario;

            if (id == default)
            {
                id = usuarioFake.Id;
            }

            if (nome == default)
            {
                nome = usuarioFake.Nome;
            }

            if (email == default)
            {
                email = usuarioFake.Email;
            }

            usuarioFake = new Usuario(id, nome, email, perfil);

            return usuarioFake;
        }
    }
}
