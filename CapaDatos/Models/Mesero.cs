using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Models
{
    public class Mesero
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMesero { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR2(100)")]
        public string Nombres { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR2(100)")]
        public string Apellidos { get; set; }
        [Required]
        public int Edad { get; set; }
        [Required]
        public int Antiguedad { get; set; }
    }
}
