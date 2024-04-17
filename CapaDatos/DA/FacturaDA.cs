using AutoMapper;
using CapaDatos.DTO;
using CapaDatos.Entidades;
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
        private readonly IMapper _mapper;

        public FacturaDA(TiendaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<FacturaDTO>> ListaXFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            var tiendaContext = _context.Facturas.Include(f => f.Cliente).Include(f => f.Mesa)
                .Include(f => f.Mesero).Include(f => f.DetalleXFacturas).ThenInclude(x => x.Producto).Where(x => x.Fecha.Date >= fechaInicio.Date && x.Fecha.Date <= fechaFin.Date);
            return _mapper.Map<List<FacturaDTO>>(await tiendaContext.ToListAsync());
        }

        public async Task<List<FacturaDTO>> FacturaXNumero(int numFactura)
        {
            var tiendaContext = _context.Facturas.Include(f => f.Cliente).Include(f => f.Mesa)
                .Include(f => f.Mesero).Include(f => f.DetalleXFacturas).ThenInclude(x=>x.Producto).Where(x=>x.NroFactura == numFactura);
            return _mapper.Map<List<FacturaDTO>>(await tiendaContext.ToListAsync());
        }

        public async Task Crear(FacturaDTO factura)
        {
            _context.Add(_mapper.Map<Factura>(factura));
            await _context.SaveChangesAsync();
        }

        public async Task<FacturaDTO> Editar(FacturaDTO factura)
        {
            try
            {
                if (!FacturaExists(factura.NroFactura))
                    throw new TaskCanceledException("La factura no existe");

                var facturaModelo = _mapper.Map<Factura>(factura);

                _context.Update(facturaModelo);

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

        public async Task<bool> Eliminar(int id)
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
