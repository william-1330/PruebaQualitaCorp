using CapaDatos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CapaDatos.DA;
using CapaDatos.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CapaNegocio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {
        private readonly MesaDA _mesaDA;

        public MesaController(MesaDA mesaDA)
        {
            _mesaDA = mesaDA;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            List<MesaDTO> listCliente = await _mesaDA.Lista();
            return new JsonResult(listCliente);
        }

        [HttpPost]
        [Route("Crear")]
        public async Task<IActionResult> Crear([FromBody] MesaDTO mesa)
        {
            try
            {
                await _mesaDA.Crear(mesa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] MesaDTO mesa)
        {
            var rsp = new MesaDTO();

            try
            {
                rsp = await _mesaDA.Editar(mesa);
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
                rsp = await _mesaDA.Eliminar(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(rsp);
        }
    }
}