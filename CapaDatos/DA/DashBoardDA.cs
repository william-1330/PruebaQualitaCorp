using AutoMapper;
using CapaDatos.DTO;
using CapaDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.DA
{
    public class DashBoardDA
    {
        private readonly TiendaContext _context;
        private readonly IMapper _mapper;

        public DashBoardDA(TiendaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DashBoardDTO> Resumen()
        {
            DashBoardDTO vmDahsBoard = new DashBoardDTO();

            try
            {
                vmDahsBoard.TotalVentas = await TotalVentasUltimaSemana();
                vmDahsBoard.TotalIngresos = await TotalIngresosUltimaSemana();
                vmDahsBoard.TotalProductos = await TotalProductos();

                List<VentasSemanaDTO> listaVentaSemana = new List<VentasSemanaDTO>();

                foreach (KeyValuePair<string, int> item in await VentasUltimaSemana())
                {

                    listaVentaSemana.Add(new VentasSemanaDTO()
                    {
                        Fecha = item.Key,
                        Total = item.Value
                    });
                }

                vmDahsBoard.VentasUltimaSemana = listaVentaSemana;
            }
            catch
            {
                throw;
            }

            return vmDahsBoard;
        }

        private IQueryable<Factura> retornarVentas(IQueryable<Factura> tablaVenta, int restarCantidadDias)
        {

            DateTime? ultimaFecha = tablaVenta.OrderByDescending(v => v.Fecha).Select(v => v.Fecha).First();

            ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);

            return tablaVenta.Where(v => v.Fecha.Date >= ultimaFecha.Value.Date);
        }

        private async Task<int> TotalVentasUltimaSemana()
        {
            int total = 0;
            //IQueryable<Factura> _ventaQuery = await _ventaRepositorio.Consultar();
            var _ventaQuery = _context.Facturas;

            if (_ventaQuery.Count() > 0)
            {
                var tablaVenta = retornarVentas(_ventaQuery, -7);
                total = tablaVenta.Count();
            }

            return total;
        }

        private async Task<string> TotalIngresosUltimaSemana()
        {

            decimal resultado = 0;
            //IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();
            var _ventaQuery = _context.Facturas;

            if (_ventaQuery.Count() > 0)
            {
                var tablaventa = retornarVentas(_ventaQuery, -7);

                resultado = tablaventa.Select(v => v.Total).Sum(v => v);
            }

            return Convert.ToString(resultado, new CultureInfo("es-CO"));

        }

        private async Task<int> TotalProductos()
        {

            //IQueryable<Producto> _productoQuery = await _productoRepositorio.Consultar();
            var _productoQuery = _context.Productos;

            int total = _productoQuery.Count();
            return total;
        }

        private async Task<Dictionary<string, int>> VentasUltimaSemana()
        {

            Dictionary<string, int> resultado = new Dictionary<string, int>();

            //IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();
            var _ventaQuery = _context.Facturas;

            if (_ventaQuery.Count() > 0)
            {

                var tablaVenta = retornarVentas(_ventaQuery, -7);

                resultado = tablaVenta
                    .GroupBy(v => v.Fecha.Date).OrderBy(g => g.Key)
                    .Select(dv => new { fecha = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
                    .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);

            }

            return resultado;
        }
    }
}
