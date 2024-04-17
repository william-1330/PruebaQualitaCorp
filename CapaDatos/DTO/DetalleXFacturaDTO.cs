using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos.Entidades;

namespace CapaDatos.DTO
{
    public class DetalleXFacturaDTO
    {
        public int IdDetalleXFactura { get; set; }

        public int NroFactura { get; set; }

        public int IdProducto { get; set; }

        public string DescripcionProducto { get; set; }

        public decimal Precio { get; set; }

        public int Cantidad { get; set; }

        public decimal Total { get; set; }
    }
}
