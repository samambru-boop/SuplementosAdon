namespace SuplementosAdon.API.DTOs
{
    public class ReporteVentasDTO
    {
        public int CantidadVentas { get; set; }

        public decimal TotalVendido { get; set; }

        public decimal GananciaEstimada { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }
    }
}