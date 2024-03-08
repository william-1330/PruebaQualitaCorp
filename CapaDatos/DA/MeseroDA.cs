using CapaDatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.DA
{
    public class MeseroDA
    {
        private readonly TiendaContext _context;

        public MeseroDA(TiendaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Mesero>> Index()
        {
            var tiendaContext = _context.Meseros;
            return await tiendaContext.ToListAsync();
        }

        public async Task<Mesero> Details(int? id)
        {
            var Mesero = await _context.Meseros
                .FirstOrDefaultAsync(m => m.IdMesero == id);
            return Mesero;
        }

        public async Task Create(Mesero Mesero)
        {
            _context.Add(Mesero);
            await _context.SaveChangesAsync();
        }

        public async Task<Mesero> Edit(int? id)
        {
            var Mesero = await _context.Meseros.FindAsync(id);
            return Mesero;
        }

        public async Task<Mesero> Edit(Mesero Mesero)
        {
            try
            {
                _context.Update(Mesero);
                await _context.SaveChangesAsync();
                return Mesero;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!MeseroExists(Mesero.IdMesero))
                {
                    return Mesero;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Mesero> Delete(int? id)
        {
            var Mesero = await _context.Meseros
                .FirstOrDefaultAsync(m => m.IdMesero == id);

            return Mesero;
        }

        public async Task<bool> DeleteConfirmed(int id)
        {
            var Mesero = await _context.Meseros.FindAsync(id);
            if (Mesero != null)
            {
                _context.Meseros.Remove(Mesero);
            }
            if (await _context.SaveChangesAsync() <= 0)
                return false;

            return true;
        }

        public bool MeseroExists(int id)
        {
            return _context.Meseros.Any(e => e.IdMesero == id);
        }
    }
}
