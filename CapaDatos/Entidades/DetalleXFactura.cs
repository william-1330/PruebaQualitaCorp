using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CapaDatos.Entidades
{
    public class DetalleXFactura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDetalleXFactura { get; set; }

        [Required]
        public int NroFactura { get; set; }

        [Required]
        public int IdProducto { get; set; }

        [Required]
        [Precision(12, 2)]
        public decimal Precio { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Precision(12, 2)]
        public decimal Total { get; set; }



        [ForeignKey("NroFactura")]
        public virtual Factura? Factura { get; set; }

        [ForeignKey("IdProducto")]
        public virtual Producto? Producto { get; set; }
    }
}
