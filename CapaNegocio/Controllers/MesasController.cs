using CapaDatos.Models;
using CapaDatos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CapaDatos.DA;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CapaNegocio.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MesasController : ControllerBase
    {
        private readonly MesaDA _mesaDA;

        public MesasController(MesaDA mesaDA)
        {
            _mesaDA = mesaDA;
        }

        // GET: Mesas
        public async Task<IActionResult> Index()
        {
            IEnumerable<Mesa> listMesa = await _mesaDA.Index();
            return new JsonResult(listMesa);
        }

        // GET: Mesas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Mesa mesa = await _mesaDA.Details(id);

            if (mesa == null)
            {
                return NotFound();
            }

            return new JsonResult(mesa);
        }

        // GET: Mesas/Create
        public IActionResult Create()
        {
            return Ok();
        }

        // POST: Mesas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] Mesa mesa)
        {
            if (ModelState.IsValid)
            {
                await _mesaDA.Create(mesa);
                return RedirectToAction(nameof(Index));
            }
            return new JsonResult(mesa);
        }

        // GET: Mesas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mesa = await _mesaDA.Edit(id);
            if (mesa == null)
            {
                return NotFound();
            }

            return new JsonResult(mesa);
        }

        // POST: Mesas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] Mesa mesa)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _mesaDA.Edit(mesa);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MesaExists(mesa.NroMesa))
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

            return new JsonResult(mesa);
        }

        // GET: Mesas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Mesa mesa = await _mesaDA.Delete(id);

            if (mesa == null)
            {
                return NotFound();
            }

            return new JsonResult(mesa);
        }


        // POST: Mesas/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromBody] int id)
        {
            bool result = await _mesaDA.DeleteConfirmed(id);

            if (result)
                return RedirectToAction(nameof(Index));
            else
                return NoContent();
        }

        private bool MesaExists(int id)
        {
            return _mesaDA.MesaExists(id);

        }
    }
}