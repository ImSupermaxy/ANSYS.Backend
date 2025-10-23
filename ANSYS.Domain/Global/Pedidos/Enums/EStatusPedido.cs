using System.ComponentModel.DataAnnotations;

namespace ANSYS.Domain.Global.Pedidos.Enums
{
    public enum EStatusPedido
    {
        [Display(Name = "Todos")]
        Todos = -1,

        [Display(Name = "Cancelado")]
        Cancelado = 0,

        [Display(Name = "Pago")]
        Pago = 1,

        [Display(Name = "Pendente")]
        PendentePagamento = 2,
    }
}
