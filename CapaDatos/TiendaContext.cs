using CapaDatos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class TiendaContext : DbContext
    {
        public TiendaContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Mesero> Meseros { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<DetalleXFactura> DetalleXFacturas { get; set; }
        public DbSet<Supervisor> Supervisores { get; set; }
    }
}
