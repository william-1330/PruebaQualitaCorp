using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.DTO
{
    public class MesaDTO
    {
        public int NroMesa { get; set; }

        public string Nombre { get; set; }

        public bool Reservada { get; set; }

        public int Puestos { get; set; }
    }
}
