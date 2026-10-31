using Microsoft.EntityFrameworkCore;
using SuplementosAdon.Desktop.Modelos;
using System.Collections.Generic;


namespace SuplementosAdon.Desktop.Datos
{
    public class SuplementosAdonContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=SuplementosAdon;Username=postgres;Password=******"
            );
        }
    }
}