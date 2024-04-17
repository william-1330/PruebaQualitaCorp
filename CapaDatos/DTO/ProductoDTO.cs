using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapaDatos.DTO;

public class ProductoDTO
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; }

    public decimal Precio { get; set; }

}
