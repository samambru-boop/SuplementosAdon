using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global::SuplementosAdon.API.Models;
using Microsoft.EntityFrameworkCore;



namespace SuplementosAdon.API.Data
{
    public class SuplementosAdonContext : DbContext
    {
        public SuplementosAdonContext(DbContextOptions<SuplementosAdonContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Compra> Compras { get; set; }

        public DbSet<DetalleCompra> DetallesCompra { get; set; }

        public DbSet<Venta> Ventas { get; set; }

        public DbSet<DetalleVenta> DetallesVenta { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
    }
}