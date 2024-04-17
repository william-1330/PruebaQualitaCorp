using CapaDatos.DA;
using CapaDatos.DTO;
using CapaDatos.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


//using SistemaVenta.BLL.Servicios.Contrato;
//using SistemaVenta.DTO;
//using SistemaVenta.API.Utilidad;

namespace CapaNegocio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly RolDA _rolDA;

        public RolController(RolDA rolDA)
        {
            _rolDA = rolDA;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista() 
        {
            var rsp = new List<RolDTO>();

            try
            {
                rsp = await _rolDA.Lista();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }

            return new JsonResult(rsp);
        }
    }
}
