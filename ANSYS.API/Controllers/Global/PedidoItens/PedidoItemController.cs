using ANSYS.Application.Global.PedidoItens.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ANSYS.API.Controllers.Global.PedidoItens
{
    [ApiController]
    [Route("api/v1/pedidoitem")]
    public class PedidoItemController : ControllerBase
    {
        public ISender Sender { get; }

        public PedidoItemController(ISender sender)
        {
            Sender = sender;
        }

        /// <summary>
        /// Obtem todos os itens dos pedidos cadastrados.
        /// </summary>
        /// <param name="cliente">Filtro com o nome do cliente</param>
        /// <param name="numeroPedido">Filtro com o identificador do item do pedido</param>
        /// <param name="status">Filtro com o status dos itens do pedido</param>
        /// <returns>Retorna uma lista dos itens dos pedidos obtidos</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Sender.Send(new PedidoItemCommandGetAll());

            if (result == default)
                return NotFound(result);

            return Ok(result);
        }

        /// <summary>
        /// Obtem um item do pedido cadastrado pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador do item do pedido</param>
        /// <returns>Retorna um item do pedido</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            var result = await Sender.Send(new PedidoItemCommandGetById(id != null ? id.Value : default!));

            if (result == default)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Obtem um item do pedido cadastrado pelo identificador do pedido.
        /// </summary>
        /// <param name="pedidoId">Identificador do pedido</param>
        /// <returns>Retorna uma lista de itens do pedido</returns>
        [HttpGet("pedido/{pedidoId}")]
        public async Task<IActionResult> GetByPedidoId(int? pedidoId)
        {
            var result = await Sender.Send(new PedidoItemCommandGetByPedidoId(pedidoId != null ? pedidoId.Value : default!));

            if (result == default)
                return NotFound();

            return Ok(result);
        }
    }
}
