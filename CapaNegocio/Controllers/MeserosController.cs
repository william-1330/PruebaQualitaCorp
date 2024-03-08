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
    public class MeserosController : ControllerBase
    {
        private readonly MeseroDA _meseroDA;

        public MeserosController(MeseroDA meseroDA)
        {
            _meseroDA = meseroDA;
        }

        // GET: Meseros
        public async Task<IActionResult> Index()
        {
            IEnumerable<Mesero> listMesero = await _meseroDA.Index();
            return new JsonResult(listMesero);
        }

        // GET: Meseros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Mesero mesero = await _meseroDA.Details(id);

            if (mesero == null)
            {
                return NotFound();
            }

            return new JsonResult(mesero);
        }

        // GET: Meseroes/Create
        public IActionResult Create()
        {
            return Ok();
        }

        // POST: Meseroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] Mesero mesero)
        {
            if (ModelState.IsValid)
            {
                await _meseroDA.Create(mesero);
                return RedirectToAction(nameof(Index));
            }
            return new JsonResult(mesero);
        }

        // GET: Meseroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mesero = await _meseroDA.Edit(id);
            if (mesero == null)
            {
                return NotFound();
            }

            return new JsonResult(mesero);
        }

        // POST: Meseroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] Mesero mesero)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _meseroDA.Edit(mesero);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeseroExists(mesero.IdMesero))
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

            return new JsonResult(mesero);
        }

        // GET: Meseroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Mesero mesero = await _meseroDA.Delete(id);

            if (mesero == null)
            {
                return NotFound();
            }

            return new JsonResult(mesero);
        }

        // POST: Meseroes/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromBody] int id)
        {
            bool result = await _meseroDA.DeleteConfirmed(id);

            if (result)
                return Ok();
            else
                return NoContent();
        }

        private bool MeseroExists(int id)
        {
            return _meseroDA.MeseroExists(id);
        }
    }
}
