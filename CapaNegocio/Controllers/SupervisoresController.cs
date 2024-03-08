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
    public class SupervisoresController : Controller
    {
        private readonly SupervisorDA _supervisorDA;

        public SupervisoresController(SupervisorDA supervisorDA)
        {
            _supervisorDA = supervisorDA;
        }

        // GET: Supervisores
        public async Task<IActionResult> Index()
        {
            IEnumerable<Supervisor> listSupervisor = await _supervisorDA.Index();
            return new JsonResult(listSupervisor);
        }

        // GET: Supervisores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Supervisor supervisor = await _supervisorDA.Details(id);

            if (supervisor == null)
            {
                return NotFound();
            }

            return new JsonResult(supervisor);
        }

        // GET: Supervisores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Supervisores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] Supervisor supervisor)
        {
            if (ModelState.IsValid)
            {
                await _supervisorDA.Create(supervisor);
                return RedirectToAction(nameof(Index));
            }
            return new JsonResult(supervisor);
        }

        // GET: Supervisores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supervisor = await _supervisorDA.Edit(id);
            if (supervisor == null)
            {
                return NotFound();
            }

            return new JsonResult(supervisor);
        }

        // POST: Supervisores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody]  Supervisor supervisor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _supervisorDA.Edit(supervisor);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupervisorExists(supervisor.IdSupervisor))
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

            return new JsonResult(supervisor);
        }

        // GET: Supervisores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Supervisor supervisor = await _supervisorDA.Delete(id);

            if (supervisor == null)
            {
                return NotFound();
            }

            return new JsonResult(supervisor);
        }

        // POST: Supervisores/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromBody] int id)
        {
            bool result = await _supervisorDA.DeleteConfirmed(id);

            if (result)
                return RedirectToAction(nameof(Index));
            else
                return NoContent();
        }

        private bool SupervisorExists(int id)
        {
            return _supervisorDA.SupervisorExists(id);
        }
    }
}
