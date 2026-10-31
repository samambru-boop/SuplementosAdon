using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuplementosAdon.API.Models
{
    public class Producto
    {
        public int Id { get; set; }

        public string Codigo { get; set; } = string.Empty;

        public string CodigoBarras { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;


        public int MarcaId { get; set; }

        public Marca? Marca { get; set; }


        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }

        public decimal PrecioCompra { get; set; }

        public decimal PrecioVenta { get; set; }

        public int Stock { get; set; }

        public int StockMinimo { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        public bool Activo { get; set; }
    }
}

