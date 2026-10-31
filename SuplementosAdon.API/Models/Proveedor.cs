
namespace SuplementosAdon.API.Models
{
    public class Proveedor
    {
        private int _id;
        private string _nombre = string.Empty;
        private string _empresa = string.Empty;
        private string _telefono = string.Empty;
        private string _email = string.Empty;
        private string _direccion = string.Empty;
        private bool _activo = true;

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

        public string Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        public bool Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }
    }
}
