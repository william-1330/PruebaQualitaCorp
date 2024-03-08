using CapaDatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.DA
{
    public class FacturaDA
    {
        private readonly TiendaContext _context;

        public FacturaDA(TiendaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Factura>> Index()
        {
            var tiendaContext = _context.Facturas.Include(f => f.Cliente).Include(f => f.Mesa).Include(f => f.Mesero);
            return await tiendaContext.ToListAsync();
        }

        public async Task<Factura> Details(int? id)
        {
            var factura = await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Mesa)
                .Include(f => f.Mesero)
                .FirstOrDefaultAsync(m => m.NroFactura == id);
            return factura;
        }

        public async Task Create(Factura factura)
        {
            _context.Add(factura);
            await _context.SaveChangesAsync();
        }

        public async Task<Factura> Edit(int? id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            return factura;
        }

        public async Task<Factura> Edit(Factura factura)
        {
            try
            {
                _context.Update(factura);
                await _context.SaveChangesAsync();
                return factura;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!FacturaExists(factura.NroFactura))
                {
                    return factura;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Factura> Delete(int? id)
        {
            var factura = await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Mesa)
                .Include(f => f.Mesero)
                .FirstOrDefaultAsync(m => m.NroFactura == id);

            return factura;
        }

        public async Task<bool> DeleteConfirmed(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
            }
            if (await _context.SaveChangesAsync() <= 0)
                return false;

            return true;
        }

        public bool FacturaExists(int id)
        {
            return _context.Facturas.Any(e => e.NroFactura == id);
        }
    }
}
