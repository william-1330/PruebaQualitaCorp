using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CapaNegocio;
using CapaDatos;
using CapaDatos.Models;
using CapaDatos.DA;

namespace CapaNegocio.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DetalleXFacturasController : Controller
    {
        private readonly DetalleXFacturaDA _detalleXFacturaDA;

        public DetalleXFacturasController(DetalleXFacturaDA detalleXFacturaDA)
        {
            _detalleXFacturaDA = detalleXFacturaDA;
        }

        // GET: DetalleXFacturas
        public async Task<IActionResult> Index()
        {
            IEnumerable<DetalleXFactura> listDetalleXFactura = await _detalleXFacturaDA.Index();
            return new JsonResult(listDetalleXFactura);
        }

        // GET: DetalleXFacturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DetalleXFactura detalleXFactura = await _detalleXFacturaDA.Details(id);

            if (detalleXFactura == null)
            {
                return NotFound();
            }

            return new JsonResult(detalleXFactura);
        }

        // GET: DetalleXFacturas/Create
        public IActionResult Create()
        {
            return Ok();
        }

        // POST: DetalleXFacturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] DetalleXFactura detalleXFactura)
        {
            if (ModelState.IsValid)
            {
                await _detalleXFacturaDA.Create(detalleXFactura);
                return Ok();
            }
            return new JsonResult(detalleXFactura);
        }

        // GET: DetalleXFacturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleXFactura = await _detalleXFacturaDA.Edit(id);
            if (detalleXFactura == null)
            {
                return NotFound();
            }
            return new JsonResult(detalleXFactura);
        }

        // POST: DetalleXFacturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] DetalleXFactura detalleXFactura)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _detalleXFacturaDA.Edit(detalleXFactura);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleXFacturaExists(detalleXFactura.IdDetalleXFactura))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return new JsonResult(detalleXFactura);
        }

        // GET: DetalleXFacturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DetalleXFactura detalleXFactura = await _detalleXFacturaDA.Delete(id);

            if (detalleXFactura == null)
            {
                return NotFound();
            }

            return new JsonResult(detalleXFactura);
        }

        // POST: DetalleXFacturas/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromBody] int id)
        {
            bool result = await _detalleXFacturaDA.DeleteConfirmed(id);

            if (result)
                return RedirectToAction(nameof(Index));
            else
                return NoContent();
        }

        private bool DetalleXFacturaExists(int id)
        {
            return _detalleXFacturaDA.DetalleXFacturaExists(id);
        }
    }
}
