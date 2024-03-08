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
    public class ClientesController : ControllerBase
    {
        private readonly ClienteDA _clienteDA;

        public ClientesController(ClienteDA clienteDA)
        {
            _clienteDA = clienteDA;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            IEnumerable<Cliente> listCliente = await _clienteDA.Index();
            return new JsonResult(listCliente);
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cliente cliente = await _clienteDA.Details(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return new JsonResult(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return Ok();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,Nombres,Apellidos,Direccion,Telefono")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                await _clienteDA.Create(cliente);
                return RedirectToAction(nameof(Index));
            }
            return new JsonResult(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _clienteDA.Edit(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return new JsonResult(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _clienteDA.Edit(cliente);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.IdCliente))
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

            return new JsonResult(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cliente cliente = await _clienteDA.Delete(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return new JsonResult(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromBody] int id)
        {
            bool result = await _clienteDA.DeleteConfirmed(id);

            if (result)
                return RedirectToAction(nameof(Index));
            else
                return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _clienteDA.ClienteExists(id);
        }
    }
}
