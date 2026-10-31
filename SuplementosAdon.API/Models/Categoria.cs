
namespace SuplementosAdon.API.Models
{
    public class Categoria
    {
        private int _id;
        private string _nombre = string.Empty;
        private string _descripcion = string.Empty;
        private bool _activo;

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
    }
}
