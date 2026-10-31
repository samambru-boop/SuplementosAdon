using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuplementosAdon.API.Data;
using SuplementosAdon.API.Models;

namespace SuplementosAdon.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcasController : ControllerBase
    {
        private readonly SuplementosAdonContext _context;

        public MarcasController(SuplementosAdonContext context)
        {
            _context = context;
        }


        // GET: api/Marcas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Marca>>> GetMarcas()
        {
            return await _context.Marcas.ToListAsync();
        }


        // GET: api/Marcas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Marca>> GetMarca(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);

            if (marca == null)
            {
                return NotFound();
            }

            return marca;
        }


        // POST: api/Marcas
        [HttpPost]
        public async Task<ActionResult<Marca>> PostMarca(Marca marca)
        {
            _context.Marcas.Add(marca);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMarca),
                new { id = marca.Id }, marca);
        }
    }
}