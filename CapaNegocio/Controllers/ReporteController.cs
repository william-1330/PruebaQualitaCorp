using CapaDatos.DA;
using CapaDatos.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Globalization;

namespace CapaNegocio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly ReporteDA _reporteDA;

        public ReporteController(ReporteDA reporteDA)
        {
            _reporteDA = reporteDA;
        }

        [HttpGet]
        [Route("TotalXMesero")]
        public IActionResult TotalXMesero(string fechaInicio, string fechaFin)
        {
            DateTime dateInicio = DateTime.Parse(fechaInicio, new CultureInfo("es-CO"));
            DateTime dateFin = DateTime.Parse(fechaFin, new CultureInfo("es-CO"));

            IEnumerable resultado = _reporteDA.TotalXMesero(dateInicio, dateFin);
            return new JsonResult(resultado);
        }

        [HttpGet]
        [Route("CunsumoXCliente")]
        public IActionResult CunsumoXCliente(string fechaInicio, string fechaFin, double valor)
        {
            DateTime dateInicio = DateTime.Parse(fechaInicio, new CultureInfo("es-CO"));
            DateTime dateFin = DateTime.Parse(fechaFin, new CultureInfo("es-CO"));

            IEnumerable resultado = _reporteDA.CunsumoXCliente(dateInicio, dateFin, (decimal)valor);
            return new JsonResult(resultado);
        }

        [HttpGet]
        [Route("PlatoMasVendido")]
        public IActionResult PlatoMasVendido(string fechaInicio, string fechaFin)
        {
            DateTime dateInicio = DateTime.Parse(fechaInicio, new CultureInfo("es-CO"));
            DateTime dateFin = DateTime.Parse(fechaFin, new CultureInfo("es-CO"));

            IEnumerable resultado = _reporteDA.PlatoMasVendido(dateInicio, dateFin);
            return new JsonResult(resultado);
        }

        [HttpGet]
        [Route("General")]
        public async Task<IActionResult> General(string fechaInicio, string fechaFin)
        {
            DateTime dateInicio = DateTime.Parse(fechaInicio, new CultureInfo("es-CO"));
            DateTime dateFin = DateTime.Parse(fechaFin, new CultureInfo("es-CO"));
            IEnumerable resultado = _reporteDA.General(dateInicio, dateFin);
            return new JsonResult(resultado);
        }
    }
}
