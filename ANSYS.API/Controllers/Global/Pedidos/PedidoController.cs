using ANSYS.Application.Global.Pedidos.Commands;
using ANSYS.Domain.Global.Pedidos.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ANSYS.API.Controllers.Global.Pedidos
{
    [ApiController]
    [Route("api/v1/pedido")]
    public class PedidoController : ControllerBase
    {
        public ISender Sender { get; }

        public PedidoController(ISender sender)
        {
            Sender = sender;
        }

        /// <summary>
        /// Obtem todos os pedidos cadastrados.
        /// </summary>
        /// <param name="cliente">Filtro com o nome do cliente</param>
        /// <param name="numeroPedido">Filtro com o identificador do pedido</param>
        /// <param name="status">Filtro com o status do pedido</param>
        /// <returns>Retorna uma lista dos pedidos obtidos</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(string? cliente = null, int? numeroPedido = null, EStatusPedido status = EStatusPedido.Todos)
        {
            var result = await Sender.Send(new PedidoCommandGetAll(cliente!, numeroPedido, status));

            if (result == default)
                return NotFound(result);

            return Ok(result);
        }

        /// <summary>
        /// Obtem um pedido cadastrado pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador do pedido</param>
        /// <returns>Retorna um pedido</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            var result = await Sender.Send(new PedidoCommandGetById(id != null ? id.Value : default!));

            if (result == default)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Insere um novo pedido
        /// </summary>
        /// <param name="command">O payload com as informações do pedido</param>
        /// <returns>Retorna um sucesso ou falha no insert</returns>
        [HttpPost]
        public async Task<IActionResult> Post(PedidoCommandVenda command)
        {
            var result = await Sender.Send(command);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        /// <summary>
        /// Atualiza um pedido existente
        /// </summary>
        /// <param name="command">O payload com as informações do pedido</param>
        /// <returns>Retorna um sucesso ou falha na atualização</returns>
        [HttpPut]
        [Authorize] //Rota inacessível por qualquer funcionário, apenas para administradores e master (perfil)
        public async Task<IActionResult> Put(PedidoCommandUpdate command)
        {
            var result = await Sender.Send(command);
            if (!result)
                return BadRequest();

            return Ok();
        }

        /// <summary>
        /// Cancela um pedido existente.
        /// </summary>
        /// <param name="command">O payload com o id do pedido a ser cancelado</param>
        /// <returns>Retorna um sucesso ou falha do cancelamento</returns>
        [HttpDelete("cancelar")]
        public async Task<IActionResult> Cancelar(PedidoCommandCancelar command)
        {
            var result = await Sender.Send(command);
            if (!result)
                return BadRequest();

            return Ok();
        }

        /// <summary>
        /// Aprova um pedido existente.
        /// </summary>
        /// <param name="command">O payload com o id do pedido a ser aprovado</param>
        /// <returns>Retorna um sucesso ou falha da aprovação</returns>
        [HttpPut("aprovar")]
        public async Task<IActionResult> Aprovar(PedidoCommandAprovar command)
        {
            var result = await Sender.Send(command);
            if (!result)
                return BadRequest();

            return Ok();
        }

    }
}
