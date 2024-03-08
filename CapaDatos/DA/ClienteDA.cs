using CapaDatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.DA
{
    public class ClienteDA
    {
        private readonly TiendaContext _context;

        public ClienteDA(TiendaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> Index()
        {
            var tiendaContext = _context.Clientes;
            return await tiendaContext.ToListAsync();
        }

        public async Task<Cliente> Details(int? id)
        {
            var Cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            return Cliente;
        }

        public async Task Create(Cliente Cliente)
        {
            _context.Add(Cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<Cliente> Edit(int? id)
        {
            var Cliente = await _context.Clientes.FindAsync(id);
            return Cliente;
        }

        public async Task<Cliente> Edit(Cliente Cliente)
        {
            try
            {
                _context.Update(Cliente);
                await _context.SaveChangesAsync();
                return Cliente;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ClienteExists(Cliente.IdCliente))
                {
                    return Cliente;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Cliente> Delete(int? id)
        {
            var Cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.IdCliente == id);

            return Cliente;
        }

        public async Task<bool> DeleteConfirmed(int id)
        {
            var Cliente = await _context.Clientes.FindAsync(id);
            if (Cliente != null)
            {
                _context.Clientes.Remove(Cliente);
            }
            if (await _context.SaveChangesAsync() <= 0)
                return false;

            return true;
        }

        public bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.IdCliente == id);
        }
    }
}
