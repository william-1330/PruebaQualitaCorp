using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Entidades
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string Nombres { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string Apellidos { get; set; }      
        
        public int Edad { get; set; }
        public int Antiguedad { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string Correo { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string Clave { get; set; }

        [Required]
        public int IdRol { get; set; }



        [ForeignKey("IdRol")]
        public virtual Rol Rol { get; set; }

    }
}
