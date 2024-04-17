using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.DTO
{
    public class VentasSemanaDTO
    {
        public string? Fecha { get; set; }
        public int Total { get; set; }
    }
}
