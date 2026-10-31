using Microsoft.AspNetCore.Mvc;
using SuplementosAdon.API.DTOs;
using SuplementosAdon.API.Interfaces;

namespace SuplementosAdon.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComprasController : ControllerBase
    {
        private readonly ICompraService _compraService;

        public ComprasController(ICompraService compraService)
        {
            _compraService = compraService;
        }


        // POST: api/Compras
        [HttpPost]
        public async Task<IActionResult> RegistrarCompra(CompraDTO compraDTO)
        {
            await _compraService.RegistrarCompraAsync(compraDTO);

            return Ok(new
            {
                mensaje = "Compra registrada correctamente"
            });
        }
    }
}