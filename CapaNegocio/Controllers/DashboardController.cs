using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CapaDatos.DA;
using CapaDatos.DTO;

namespace CapaNegocio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DashBoardDA _dashBoardDA;

        public DashboardController(DashBoardDA dashBoardDA)
        {
            _dashBoardDA = dashBoardDA;
        }

        [HttpGet]
        [Route("Resumen")]
        public async Task<IActionResult> Resumen()
        {
            var rsp = new DashBoardDTO();

            try
            {
                //rsp.status = true;
                rsp = await _dashBoardDA.Resumen();

            }
            catch (Exception ex)
            {
                //rsp.status = false;
                //rsp.msg = ex.Message;

            }
            return new JsonResult(rsp);
        }
    }
}
