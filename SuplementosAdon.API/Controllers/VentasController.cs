using Microsoft.AspNetCore.Mvc;
using SuplementosAdon.API.DTOs;
using SuplementosAdon.API.Interfaces;

namespace SuplementosAdon.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentasController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarVenta(VentaDTO ventaDTO)
        {
            await _ventaService.RegistrarVentaAsync(ventaDTO);

            return Ok(new
            {
                mensaje = "Venta registrada correctamente"
            });

        }

        [HttpGet]
        public async Task<IActionResult> ObtenerVentas()
        {
            var ventas = await _ventaService.ObtenerVentasAsync();

            return Ok(ventas);
        }
    }
    
}
