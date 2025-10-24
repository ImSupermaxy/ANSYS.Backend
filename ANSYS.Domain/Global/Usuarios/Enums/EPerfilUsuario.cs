using System.ComponentModel.DataAnnotations;

namespace ANSYS.Domain.Global.Usuarios.Enums
{
    public enum EPerfilUsuario
    {
        [Display(Name = "Todos")]
        Todos = -1,

        [Display(Name = "SemPerfil")]
        SemPerfil = 0,

        [Display(Name = "Administrador")]
        Administrador = 1,

        [Display(Name = "Funcionario")]
        Funcionario = 2,

        [Display(Name = "Cliente")]
        Cliente = 3,
    }
}
