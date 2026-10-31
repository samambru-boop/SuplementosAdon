namespace SuplementosAdon.API.DTOs
{
    public class CompraDTO
    {
        public int ProveedorId { get; set; }

        public List<DetalleCompraDTO> Detalles { get; set; } = new();
    }
}