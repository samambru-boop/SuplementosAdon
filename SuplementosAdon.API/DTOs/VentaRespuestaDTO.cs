namespace SuplementosAdon.API.DTOs
{
    public class VentaRespuestaDTO
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public string Cliente { get; set; } = string.Empty;

        public decimal Total { get; set; }

        public List<DetalleVentaRespuestaDTO> Detalles { get; set; } = new();
    }
}