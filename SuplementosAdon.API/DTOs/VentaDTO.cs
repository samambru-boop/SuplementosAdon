namespace SuplementosAdon.API.DTOs
{
    public class VentaDTO
    {
        public int ClienteId { get; set; }

        public List<DetalleVentaDTO> Detalles { get; set; } = new();
    }
}