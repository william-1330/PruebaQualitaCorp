using CapaDatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.DA
{
    public class MesaDA
    {
        private readonly TiendaContext _context;

        public MesaDA(TiendaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Mesa>> Index()
        {
            var tiendaContext = _context.Mesas;
            return await tiendaContext.ToListAsync();
        }

        public async Task<Mesa> Details(int? id)
        {
            var Mesa = await _context.Mesas
                .FirstOrDefaultAsync(m => m.NroMesa == id);
            return Mesa;
        }

        public async Task Create(Mesa Mesa)
        {
            _context.Add(Mesa);
            await _context.SaveChangesAsync();
        }

        public async Task<Mesa> Edit(int? id)
        {
            var Mesa = await _context.Mesas.FindAsync(id);
            return Mesa;
        }

        public async Task<Mesa> Edit(Mesa Mesa)
        {
            try
            {
                _context.Update(Mesa);
                await _context.SaveChangesAsync();
                return Mesa;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!MesaExists(Mesa.NroMesa))
                {
                    return Mesa;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Mesa> Delete(int? id)
        {
            var Mesa = await _context.Mesas
                .FirstOrDefaultAsync(m => m.NroMesa == id);

            return Mesa;
        }

        public async Task<bool> DeleteConfirmed(int id)
        {
            var Mesa = await _context.Mesas.FindAsync(id);
            if (Mesa != null)
            {
                _context.Mesas.Remove(Mesa);
            }
            if (await _context.SaveChangesAsync() <= 0)
                return false;

            return true;
        }

        public bool MesaExists(int id)
        {
            return _context.Mesas.Any(e => e.NroMesa == id);
        }
    }
}
