using SuplementosAdon.API.DTOs;

namespace SuplementosAdon.API.Interfaces
{
    public interface IReporteService
    {
        Task<ReporteVentasDTO> ObtenerReporteVentasAsync(
            DateTime fechaInicio,
            DateTime fechaFin );

        Task<List<ReporteStockDTO>> ObtenerProductosStockBajoAsync();

        Task<List<ReporteVencimientoDTO>> ObtenerProductosProximosAVencerAsync(int dias);

        Task<DashboardDTO> ObtenerDashboardAsync();

    }
}