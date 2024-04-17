using AutoMapper;
using CapaDatos.DA;
using CapaDatos.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace CapaNegocio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoDA _productoDA;

        public ProductoController(ProductoDA productoDA)
        {
            _productoDA = productoDA;            
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new List<ProductoDTO>();

            try
            {
                rsp = await _productoDA.Lista();
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }

            return new JsonResult(rsp);
        }

        [HttpPost]
        [Route("Crear")]
        public async Task<IActionResult> Crear([FromBody] ProductoDTO producto)
        {
            try
            {
                await _productoDA.Crear(producto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] ProductoDTO producto)
        {
            var rsp = new ProductoDTO();

            try
            {
                rsp = await _productoDA.Editar(producto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
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
                rsp = await _productoDA.Eliminar(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(rsp);
        }
    }
}
