
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SuplementosAdon.Desktop.Datos;
using SuplementosAdon.Desktop.Modelos;

namespace SuplementosAdon.Desktop.Servicios
{
    public class CategoriaService
    {
        public List<Categoria> ObtenerTodas()
        {
            using (var db = new SuplementosAdonContext())
            {
                return db.Categorias
                    .AsNoTracking()
                    .OrderBy(c => c.Nombre)
                    .ToList();
            }
        }
    }
}
