using CapaDatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.DA
{
    public class DetalleXFacturaDA
    {
        private readonly TiendaContext _context;

        public DetalleXFacturaDA(TiendaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DetalleXFactura>> Index()
        {
            var tiendaContext = _context.DetalleXFacturas.Include(d => d.Factura).Include(d => d.Supervisor);
            return await tiendaContext.ToListAsync();
        }

        public async Task<DetalleXFactura> Details(int? id)
        {
            var DetalleXFactura = await _context.DetalleXFacturas
                .Include(d => d.Factura)
                .Include(d => d.Supervisor)
                .FirstOrDefaultAsync(m => m.IdDetalleXFactura == id);
            return DetalleXFactura;
        }

        public async Task Create(DetalleXFactura DetalleXFactura)
        {
            _context.Add(DetalleXFactura);
            await _context.SaveChangesAsync();
        }

        public async Task<DetalleXFactura> Edit(int? id)
        {
            var DetalleXFactura = await _context.DetalleXFacturas.FindAsync(id);
            return DetalleXFactura;
        }

        public async Task<DetalleXFactura> Edit(DetalleXFactura DetalleXFactura)
        {
            try
            {
                _context.Update(DetalleXFactura);
                await _context.SaveChangesAsync();
                return DetalleXFactura;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!DetalleXFacturaExists(DetalleXFactura.IdDetalleXFactura))
                {
                    return DetalleXFactura;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<DetalleXFactura> Delete(int? id)
        {
            var DetalleXFactura = await _context.DetalleXFacturas
                .Include(d => d.Factura)
                .Include(d => d.Supervisor)
                .FirstOrDefaultAsync(m => m.IdDetalleXFactura == id);

            return DetalleXFactura;
        }

        public async Task<bool> DeleteConfirmed(int id)
        {
            var DetalleXFactura = await _context.DetalleXFacturas.FindAsync(id);
            if (DetalleXFactura != null)
            {
                _context.DetalleXFacturas.Remove(DetalleXFactura);
            }
            if (await _context.SaveChangesAsync() <= 0)
                return false;

            return true;
        }

        public bool DetalleXFacturaExists(int id)
        {
            return _context.DetalleXFacturas.Any(e => e.IdDetalleXFactura == id);
        }
    }
}
