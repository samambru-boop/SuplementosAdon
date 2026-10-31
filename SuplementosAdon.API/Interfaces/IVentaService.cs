using SuplementosAdon.API.DTOs;

namespace SuplementosAdon.API.Interfaces
{
    public interface IVentaService
    {
        Task RegistrarVentaAsync(VentaDTO ventaDTO);

        Task<List<VentaRespuestaDTO>> ObtenerVentasAsync();
    }
}