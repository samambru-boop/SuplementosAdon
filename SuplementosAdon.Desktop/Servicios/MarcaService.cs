
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SuplementosAdon.Desktop.Datos;
using SuplementosAdon.Desktop.Modelos;

namespace SuplementosAdon.Desktop.Servicios
{
    public class MarcaService
    {
        public List<Marca> ObtenerTodas()
        {
            using (var db = new SuplementosAdonContext())
            {
                return db.Marcas
                    .AsNoTracking()
                    .Where(m => m.Activo)
                    .OrderBy(m => m.Nombre)
                    .ToList();
            }
        }
    }
}
