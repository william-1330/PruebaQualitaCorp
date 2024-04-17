using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using CapaDatos.DTO;
using Microsoft.EntityFrameworkCore;

namespace CapaDatos.Entidades
{
    public class Factura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NroFactura { get; set; }

        [Required]
        public int IdCliente { get; set; }

        [Required]
        public int? NroMesa { get; set; }

        [Required]
        public int? IdMesero { get; set; }

        [Required]
        public int? IdSupervisor { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [Precision(12, 2)]
        public decimal Total { get; set; }



        [ForeignKey("IdCliente")]
        public virtual Cliente? Cliente { get; set; }

        [ForeignKey("NroMesa")]
        public virtual Mesa? Mesa { get; set; }

        [ForeignKey("IdMesero")]
        public virtual Usuario? Mesero { get; set; }

        [ForeignKey("IdSupervisor")]
        public virtual Usuario? Supervisor { get; set; }

        public virtual ICollection<DetalleXFactura> DetalleXFacturas { get; set; }
    }
}
