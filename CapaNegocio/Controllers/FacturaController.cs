using CapaDatos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using CapaDatos.DA;
using CapaDatos.DTO;
using CapaDatos.Entidades;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CapaNegocio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly FacturaDA _facturaDA;

        public FacturaController(FacturaDA facturaDA)
        {
            _facturaDA = facturaDA;
        }

        [HttpGet]
        [Route("ListaXFecha")]
        public async Task<IActionResult> ListaXFecha(string fechaInicio, string fechaFin)
        {
            DateTime dateInicio = DateTime.Parse(fechaInicio);
            DateTime dateFin = DateTime.Parse(fechaFin);
            IEnumerable<FacturaDTO> listFactura = await _facturaDA.ListaXFecha(dateInicio, dateFin);
            return new JsonResult(listFactura);
        }

        [HttpGet]
        [Route("FacturaXNumero")]
        public async Task<IActionResult> FacturaXNumero(string numFactura)
        {
            IEnumerable<FacturaDTO> listFactura = await _facturaDA.FacturaXNumero(Int32.Parse(numFactura));
            return new JsonResult(listFactura);
        }

        [HttpPost]
        [Route("Crear")]
        public async Task<IActionResult> Crear([FromBody] FacturaDTO factura)
        {
            try
            {
                await _facturaDA.Crear(factura);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        public async Task<IActionResult> Editar([FromBody] FacturaDTO factura)
        {
            var rsp = new FacturaDTO();

            try
            {
                rsp = await _facturaDA.Editar(factura);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }

        public async Task<IActionResult> Eliminar([FromBody] int id)
        {
            var rsp = false;

            try
            {
                rsp = await _facturaDA.Eliminar(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(rsp);
        }

    }
}
