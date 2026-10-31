namespace SuplementosAdon.API.DTOs
{
    public class ReporteVencimientoDTO
    {
        public int Id { get; set; }

        public string Codigo { get; set; } = string.Empty;

        public string Producto { get; set; } = string.Empty;

        public DateTime FechaVencimiento { get; set; }

        public int DiasRestantes { get; set; }
    }
}