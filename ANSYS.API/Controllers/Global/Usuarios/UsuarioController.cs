using ANSYS.Application.Global.Usuarios.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Sender.Send(new UsuarioCommandGetAll());

            if (result == default)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Sender.Send(new UsuarioCommandGetById(id >= 0 ? (uint)id : default!));

            if (result == default)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(UsuarioCommandInsert command)
        {
            var result = await Sender.Send(command);
            if (!result)
                return BadRequest();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(UsuarioCommandUpdate command)
        {
            var result = await Sender.Send(command);
            if (!result)
                return BadRequest();

            return Ok();
        }

        #endregion

    }
}
