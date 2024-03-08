using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Models
{
    public class Mesa
    {
        [Key]
        public int NroMesa { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR2(100)")]
        public string Nombre { get; set; }
        [Required]
        public bool Reservada { get; set; }
        [Required]
        public int Puestos { get; set; }
    }
}
