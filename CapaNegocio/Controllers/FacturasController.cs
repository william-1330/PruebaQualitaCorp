using CapaDatos.Models;
using CapaDatos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using CapaDatos.DA;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CapaNegocio.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly FacturaDA _facturaDA;

        public FacturasController(FacturaDA facturaDA)
        {
            _facturaDA = facturaDA;
        }

        // GET: Facturas
        public async Task<JsonResult> Index()
        //public async Task<IActionResult> Index()
        {
            IEnumerable<Factura> listFactura = await _facturaDA.Index();
            return new JsonResult(listFactura);
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Factura factura = await _facturaDA.Details(id);

            if (factura == null)
            {
                return NotFound();
            }

            return new JsonResult(factura);
        }

        // GET: Facturas/Create
        public IActionResult Create()
        {
            return Ok();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] Factura factura)
        {
            if (ModelState.IsValid)
            {
                await _facturaDA.Create(factura);
                return Ok();
            }

            return new JsonResult(factura);
        }

        // GET: Facturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _facturaDA.Edit(id);
            if (factura == null)
            {
                return NotFound();
            }

            return new JsonResult(factura);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] Factura factura)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _facturaDA.Edit(factura);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.NroFactura))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok();
            }

            return new JsonResult(factura);
        }

        // GET: Facturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Factura factura = await _facturaDA.Delete(id);

            if (factura == null)
            {
                return NotFound();
            }

            return new JsonResult(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromBody] int id)
        {
            bool result = await _facturaDA.DeleteConfirmed(id);

            if (result)
                return Ok();
            else
                return NoContent();
        }

        private bool FacturaExists(int id)
        {
            return _facturaDA.FacturaExists(id);
        }
    }
}
