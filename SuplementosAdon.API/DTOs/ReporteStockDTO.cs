namespace SuplementosAdon.API.DTOs
{
    public class ReporteStockDTO
    {
        public int Id { get; set; }

        public string Codigo { get; set; } = string.Empty;

        public string Producto { get; set; } = string.Empty;

        public int Stock { get; set; }

        public int StockMinimo { get; set; }

        public bool NecesitaReposicion => Stock <= StockMinimo;
    }
}