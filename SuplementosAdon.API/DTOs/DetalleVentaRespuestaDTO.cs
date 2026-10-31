namespace SuplementosAdon.API.DTOs
{
    public class DetalleVentaRespuestaDTO
    {
        public string Producto { get; set; } = string.Empty;

        public int Cantidad { get; set; }

        public decimal PrecioVenta { get; set; }

        public decimal Subtotal { get; set; }
    }
}