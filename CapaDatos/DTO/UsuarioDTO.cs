using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.DTO
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public int Edad { get; set; }

        public int Antiguedad { get; set; }

        public string Correo { get; set; }

        public string Clave { get; set; }

        public int IdRol { get; set; }

        public string RolDescripcion { get; set; }

    }
}
