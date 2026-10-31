using Microsoft.AspNetCore.Mvc;
using SuplementosAdon.API.Interfaces;

namespace SuplementosAdon.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly IReporteService _reporteService;


        public ReportesController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }


        // GET: api/Reportes/Ventas
        [HttpGet("Ventas")]
        public async Task<IActionResult> ObtenerReporteVentas(
     DateTime fechaInicio,
     DateTime fechaFin)
        {
            fechaInicio = DateTime.SpecifyKind(
                fechaInicio,
                DateTimeKind.Utc
            );

            fechaFin = DateTime.SpecifyKind(
                fechaFin,
                DateTimeKind.Utc
            );


            var reporte = await _reporteService
                .ObtenerReporteVentasAsync(
                    fechaInicio,
                    fechaFin
                );


            return Ok(reporte);
        }
        // GET: api/Reportes/StockBajo
        [HttpGet("StockBajo")]
        public async Task<IActionResult> ObtenerProductosStockBajo()
        {
            var productos = await _reporteService.ObtenerProductosStockBajoAsync();

            return Ok(productos);
        }
        // GET: api/Reportes/Vencimientos
        [HttpGet("Vencimientos")]
        public async Task<IActionResult> ObtenerProductosProximosAVencer(int dias = 30)
        {
            var productos = await _reporteService.ObtenerProductosProximosAVencerAsync(dias);

            return Ok(productos);
        }
        // GET: api/Reportes/Dashboard
        [HttpGet("Dashboard")]
        public async Task<IActionResult> ObtenerDashboard()
        {
            var dashboard = await _reporteService.ObtenerDashboardAsync();

            return Ok(dashboard);
        }
    }
    
}