using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapaDatos.Entidades;

public class Producto
{
    [Key]
    public int IdProducto { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(100)")]
    public string Nombre { get; set; }

    [Required]
    [Precision(12, 2)]
    public decimal Precio { get; set; }

    [Required]
    public DateTime Fecha { get; set; }

    //public virtual ICollection<DetalleXFactura> DetalleXFacturas { get; } = new List<DetalleXFactura>();

}
