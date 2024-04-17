using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using CapaDatos.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CapaDatos.DTO
{
    public class FacturaDTO
    {
        public int NroFactura { get; set; }

        public int IdCliente { get; set; }

        public int NroMesa { get; set; }

        public int IdMesero { get; set; }

        public int IdSupervisor { get; set; }

        public string Fecha { get; set; }

        public decimal Total { get; set; }

        public virtual ICollection<DetalleXFacturaDTO> DetalleXFacturas { get; set; }
    }
}
