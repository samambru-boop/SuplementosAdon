using SuplementosAdon.API.DTOs;
using SuplementosAdon.API.Models;

namespace SuplementosAdon.API.Interfaces
{
    public interface ICompraService
    {
        Task RegistrarCompraAsync(CompraDTO compraDTO);


    }
}
