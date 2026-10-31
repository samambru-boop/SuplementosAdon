using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuplementosAdon.Desktop.Modelos
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Codigo { get; set; } = "";

        [Required]
        public string Nombre { get; set; } = "";

        public decimal PrecioCompra { get; set; }

        public decimal PrecioVenta { get; set; }

        public int Stock { get; set; }

        public int StockMinimo { get; set; }

        public bool Activo { get; set; } = true;

        public string? CodigoBarras { get; set; }

        public string? Descripcion { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        public int CategoriaId { get; set; }

        public int? MarcaId { get; set; }
    }
}