using CapaDatos.DA;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace CapaNegocio.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly ReporteDA _reporteDA;

        public ReportesController(ReporteDA reporteDA)
        {
            _reporteDA = reporteDA;
        }

        public IActionResult TotalXMesero(int meses)
        {
            DateTime fecha = DateTime.Today.AddMonths(-meses);
            IEnumerable resultado = _reporteDA.TotalXMesero(fecha);
            return new JsonResult(resultado);
        }

        public IActionResult CunsumoXCliente(int meses, double valor)
        {
            DateTime fecha = DateTime.Today.AddMonths(-meses);
            IEnumerable resultado = _reporteDA.CunsumoXCliente(fecha, valor);
            return new JsonResult(resultado);
        }

        public IActionResult PlatoMasVendido(int meses)
        {
            DateTime fecha = DateTime.Today.AddMonths(-meses);
            IEnumerable resultado = _reporteDA.PlatoMasVendido(fecha);
            return new JsonResult(resultado);
        }
    }
}
