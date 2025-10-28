using ANSYS.Application.Global.Pedidos.Commands;
using ANSYS.Application.Global.Usuarios.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ANSYS.API.Controllers.Global.Usuarios
{
    [ApiController]
    [Route("api/v1/usuario")]
    public class UsuarioController : ControllerBase
    {
        public ISender Sender { get; }

        public UsuarioController(ISender sender)
        {
            Sender = sender;
        }

        #region Endpoints for DEV

        /// <summary>
        /// Obtem todos os usuários cadastrados.
        /// </summary>
        /// <returns>Retorna uma lista dos usuários obtidos</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Sender.Send(new UsuarioCommandGetAll());

            if (result == default)
                return NotFound(result);

            return Ok(result);
        }

        /// <summary>
        /// Obtem um usuário cadastrado pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <returns>Retorna um usuário</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            var result = await Sender.Send(new UsuarioCommandGetById(id != null ? id.Value : default!));

            if (result == default)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Insere um novo usuário
        /// </summary>
        /// <param name="command">O payload com as informações do usuário</param>
        /// <returns>Retorna um sucesso ou falha no insert</returns>
        [HttpPost]
        public async Task<IActionResult> Post(UsuarioCommandInsert command)
        {
            var result = await Sender.Send(command);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        /// <summary>
        /// Atualiza um usuário existente
        /// </summary>
        /// <param name="command">O payload com as informações do usuário</param>
        /// <returns>Retorna um sucesso ou falha na atualização</returns>
        [HttpPut]
        public async Task<IActionResult> Put(UsuarioCommandUpdate command)
        {
            var result = await Sender.Send(command);
            if (result == null)
                return BadRequest();

            if (!result.Value)
                return NotFound();

            return Ok();
        }

        #endregion

    }
}
