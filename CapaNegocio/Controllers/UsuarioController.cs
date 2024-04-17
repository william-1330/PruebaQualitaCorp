using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CapaDatos.DA;
using CapaDatos.DTO;

namespace CapaNegocio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioDA _usuarioDA;

        public UsuarioController(UsuarioDA usuarioDA)
        {
            _usuarioDA = usuarioDA;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new List<UsuarioDTO>();

            try
            {
                rsp = await _usuarioDA.Lista();
            }
            catch (Exception ex)
            {
                //return fa()
                return BadRequest(ex.Message);
            }
            return new JsonResult(rsp);
        }

        [HttpPost]
        [Route("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] LoginDTO login)
        {
            var rsp = new SesionDTO();

            try
            {
                rsp = await _usuarioDA.ValidarCredenciales(login.Correo, login.Clave);
            }
            catch (Exception ex)
            {
                //rsp.status = false;
                //rsp.msg = ex.Message;
                return BadRequest(ex.Message);
            }
            return new JsonResult(rsp);

        }

        [HttpPost]
        [Route("Crear")]
        public async Task<IActionResult> Crear([FromBody] UsuarioDTO usuario)
        {
            //var rsp = new Usuario();

            try
            {
                await _usuarioDA.Crear(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                //rsp.status = false;
                //rsp.msg = ex.Message;

            }
            return Ok();

        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] UsuarioDTO usuario)
        {
            var rsp = new UsuarioDTO();

            try
            {
                //rsp.status = true;
                rsp = await _usuarioDA.Editar(usuario);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                //rsp.status = false;
                //rsp.msg = ex.Message;

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
                rsp = await _usuarioDA.Eliminar(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(rsp);
        }
    }
}
