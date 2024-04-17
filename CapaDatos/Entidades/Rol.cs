using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Entidades
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }

        [Required]
        public string Nombre { get; set; }

        //public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
    }
}
