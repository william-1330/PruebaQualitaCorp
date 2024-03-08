using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Models
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCliente { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR2(100)")]
        public string Nombres { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR2(100)")]
        public string Apellidos { get; set; }
        [Column(TypeName = "VARCHAR2(100)")]
        public string? Direccion { get; set; }
        public int? Telefono { get; set; }
    }
}
