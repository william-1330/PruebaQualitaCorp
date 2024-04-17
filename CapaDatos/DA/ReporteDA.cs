using CapaDatos.Entidades;
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

        public IEnumerable TotalXMesero(DateTime fechaInicio, DateTime fechaFin)
        {
            var result = from m in _context.Usuarios.DefaultIfEmpty()

                         join f in _context.Facturas.DefaultIfEmpty().Where(x => x.Fecha.Date >= fechaInicio.Date && x.Fecha.Date <= fechaFin.Date) on m.IdUsuario equals f.IdMesero into fs
                         from fitem in fs.DefaultIfEmpty()

                         join d in _context.DetalleXFacturas.DefaultIfEmpty() on fitem.NroFactura equals d.NroFactura into ds
                         from ditem in ds.DefaultIfEmpty()

                         group ditem by new { m.IdUsuario, m.Nombres, m.Apellidos } into g
                         select new
                         {
                             IdMesero = g.Key.IdUsuario,
                             Nombres = g.Key.Nombres,
                             Apellidos = g.Key.Apellidos,
                             Valor = g.Sum(d => d.Total)
                         };

            return result;
        }

        public IEnumerable CunsumoXCliente(DateTime fechaInicio, DateTime fechaFin, decimal valor)
        {
            var result = from c in _context.Clientes.DefaultIfEmpty()
                         join f in _context.Facturas.DefaultIfEmpty().Where(x=> x.Fecha.Date >= fechaInicio.Date && x.Fecha.Date <= fechaFin.Date) on c.IdCliente equals f.IdCliente
                         join d in _context.DetalleXFacturas on f.NroFactura equals d.NroFactura
                         group d by new { c.IdCliente, c.Nombres, c.Apellidos } into g
                         select new
                         {
                             IdCliente = g.Key.IdCliente,
                             Nombres = g.Key.Nombres,
                             Apellidos = g.Key.Apellidos,
                             Valor = g.Sum(d => d.Total)
                         };

            result = result.Where(x => x.Valor >= valor);

            return result;
        }

        public IEnumerable PlatoMasVendido(DateTime fechaInicio, DateTime fechaFin)
        {
            var result = from d in _context.DetalleXFacturas
                         join f in _context.Facturas.Where(x => x.Fecha.Date >= fechaInicio.Date && x.Fecha.Date <= fechaFin.Date) on d.NroFactura equals f.NroFactura
                         join p in _context.Productos on d.IdProducto equals p.IdProducto
                         group d by new { p.Nombre } into g
                         select new
                         {
                             Plato = g.Key.Nombre,
                             Valor = g.Sum(d => d.Total),
                             Items = g.Count()
                         };

            var maxValue = result.Max(x => x.Valor);
            return result.Where(x => x.Valor == maxValue);

        }

        public IEnumerable General(DateTime fechaInicio, DateTime fechaFin)
        {
            var result = from f in _context.Facturas.Where(x => x.Fecha.Date >= fechaInicio.Date && x.Fecha.Date <= fechaFin.Date)
                         join m in _context.Usuarios on f.IdMesero equals m.IdUsuario
                         join s in _context.Usuarios on f.IdSupervisor equals s.IdUsuario
                         join d in _context.DetalleXFacturas on f.NroFactura equals d.NroFactura
                         join p in _context.Productos on d.IdProducto equals p.IdProducto
                         orderby f.NroFactura
                         select new
                         {
                             NroFactura = f.NroFactura,
                             Fecha = f.Fecha,
                             Producto = p.Nombre,
                             Precio = d.Precio,
                             Cantidad = d.Cantidad,
                             Total = d.Total,
                             NombreMesero = m.Nombres + " " + m.Apellidos,
                             NombreSupervisor = s.Nombres + " " + s.Apellidos
                         };

            return result;

        }
    }
}