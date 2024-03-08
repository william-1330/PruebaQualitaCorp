using CapaDatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.DA
{
    public class ReporteDA
    {
        private readonly TiendaContext _context;

        public ReporteDA(TiendaContext context)
        {
            _context = context;
        }

        public IEnumerable TotalXMesero(DateTime fecha)
        {
            var result = from m in _context.Meseros.DefaultIfEmpty()

                                 join f in _context.Facturas.DefaultIfEmpty().Where(x => x.Fecha >= fecha) on m.IdMesero equals f.IdMesero into fs
                                 from fitem in fs.DefaultIfEmpty()

                                 join d in _context.DetalleXFacturas.DefaultIfEmpty() on fitem.NroFactura equals d.NroFactura into ds
                                 from ditem in ds.DefaultIfEmpty()

                                 group ditem by new { m.IdMesero, m.Nombres, m.Apellidos } into g
                                 select new
                                 {
                                     IdMesero = g.Key.IdMesero,
                                     Nombres = g.Key.Nombres,
                                     Apellidos = g.Key.Apellidos,
                                     Valor = g.Sum(d => d.Valor)
                                 };

            return result;
        }

        public IEnumerable CunsumoXCliente(DateTime fecha, double valor)
        {
            var result = from c in _context.Clientes.DefaultIfEmpty()
                                 join f in _context.Facturas.DefaultIfEmpty().Where(x => x.Fecha >= fecha) on c.IdCliente equals f.IdCliente
                                 join d in _context.DetalleXFacturas on f.NroFactura equals d.NroFactura
                                 group d by new { c.IdCliente, c.Nombres, c.Apellidos } into g
                                 select new
                                 {
                                     IdCliente = g.Key.IdCliente,
                                     Nombres = g.Key.Nombres,
                                     Apellidos = g.Key.Apellidos,
                                     Valor = g.Sum(d => d.Valor)
                                 };

            result = result.Where(x => x.Valor >= valor);

            return result;
        }

        public IEnumerable PlatoMasVendido(DateTime fecha)
        {
            var result = from d in _context.DetalleXFacturas
                         join f in _context.Facturas.Where(x => x.Fecha >= fecha) on d.NroFactura equals f.NroFactura
                         group d by new { d.Plato } into g
                         select new
                         {
                             Plato = g.Key.Plato,
                             Valor = g.Sum(d => d.Valor),
                             Items = g.Count()
                         };

            var maxValue = result.Max(x => x.Valor);
            return result.Where(x => x.Valor == maxValue);

        }
    }
}