using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.DTO
{
    public class ReporteDTO
    {

    }

    public class ConsumoXClienteViewModel
    {
        public int IdCliente { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public double Valor { get; set; }
    }

    public class PlatoMasVendidoViewModel
    {
        public string Plato { get; set; }
        public double Valor { get; set; }
        public int Items { get; set; }
    }

    public class TotalXMeseroViewModel
    {
        public int IdMesero { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public double Valor { get; set; }
    }
}
