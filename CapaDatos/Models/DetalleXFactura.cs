using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Models
{
    public class DetalleXFactura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDetalleXFactura { get; set; }
        [Required]
        public int NroFactura { get; set; }
        [Required]
        public int IdSupervisor { get; set; }
        [Required]
        public string Plato { get; set; }
        [Required]
        public double Valor { get; set; }

        [ForeignKey("NroFactura")]
        public virtual Factura? Factura { get; set; }
        [ForeignKey("IdSupervisor")]
        public virtual Supervisor? Supervisor { get; set; }

    }
}
