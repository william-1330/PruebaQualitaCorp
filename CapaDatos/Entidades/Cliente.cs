using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Entidades
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(20)")]
        public string Cedula { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string Nombres { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string Apellidos { get; set; }

        [Column(TypeName = "VARCHAR(100)")]
        public string? Direccion { get; set; }

        [Column(TypeName = "VARCHAR(10)")]
        public string? Telefono { get; set; }
    }
}
