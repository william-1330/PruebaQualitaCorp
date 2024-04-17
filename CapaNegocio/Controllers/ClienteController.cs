using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CapaDatos.DA;
using CapaDatos.DTO;

namespace CapaNegocio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteDA _clienteDA;

        public ClienteController(ClienteDA clienteDA)
        {
            _clienteDA = clienteDA;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            List<ClienteDTO> listCliente = await _clienteDA.Lista();
            return new JsonResult(listCliente);
        }

        [HttpPost]
        [Route("Crear")]
        public async Task<IActionResult> Crear([FromBody] ClienteDTO cliente)
        {
            try
            {
                await _clienteDA.Crear(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] ClienteDTO cliente)
        {
            var rsp = new ClienteDTO();

            try
            {
                rsp = await _clienteDA.Editar(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return new JsonResult(rsp);
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var rsp = false;

            try
            {
                rsp = await _clienteDA.Eliminar(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(rsp);
        }
    }
}
