using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.DTO
{
    public class ClienteDTO
    {
        public int IdCliente { get; set; }

        public string Cedula { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string? Direccion { get; set; }

        public string? Telefono { get; set; }
    }
}
