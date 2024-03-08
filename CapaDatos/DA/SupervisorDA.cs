using CapaDatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.DA
{
    public class SupervisorDA
    {
        private readonly TiendaContext _context;

        public SupervisorDA(TiendaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supervisor>> Index()
        {
            var tiendaContext = _context.Supervisores;
            return await tiendaContext.ToListAsync();
        }

        public async Task<Supervisor> Details(int? id)
        {
            var Supervisor = await _context.Supervisores
                .FirstOrDefaultAsync(m => m.IdSupervisor == id);
            return Supervisor;
        }

        public async Task Create(Supervisor Supervisor)
        {
            _context.Add(Supervisor);
            await _context.SaveChangesAsync();
        }

        public async Task<Supervisor> Edit(int? id)
        {
            var Supervisor = await _context.Supervisores.FindAsync(id);
            return Supervisor;
        }

        public async Task<Supervisor> Edit(Supervisor Supervisor)
        {
            try
            {
                _context.Update(Supervisor);
                await _context.SaveChangesAsync();
                return Supervisor;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!SupervisorExists(Supervisor.IdSupervisor))
                {
                    return Supervisor;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Supervisor> Delete(int? id)
        {
            var Supervisor = await _context.Supervisores
                .FirstOrDefaultAsync(m => m.IdSupervisor == id);

            return Supervisor;
        }

        public async Task<bool> DeleteConfirmed(int id)
        {
            var Supervisor = await _context.Supervisores.FindAsync(id);
            if (Supervisor != null)
            {
                _context.Supervisores.Remove(Supervisor);
            }
            if (await _context.SaveChangesAsync() <= 0)
                return false;

            return true;
        }

        public bool SupervisorExists(int id)
        {
            return _context.Supervisores.Any(e => e.IdSupervisor == id);
        }
    }
}
