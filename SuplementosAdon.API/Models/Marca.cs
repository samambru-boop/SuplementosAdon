
using System.Collections.Generic;

namespace SuplementosAdon.API.Models
{
    public class Marca
    {
        private int _id;
        private string _nombre = string.Empty;
        private string _descripcion = string.Empty;
        private bool _activo;
        private ICollection<Producto> _productos = new List<Producto>();

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public bool Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }

        public ICollection<Producto> Productos
        {
            get { return _productos; }
            set { _productos = value; }
        }
    }
}
