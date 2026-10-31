
namespace SuplementosAdon.API.Models
{
    public class DetalleCompra
    {
        private int _id;
        private int _compraId;
        private Compra? _compra;
        private int _productoId;
        private Producto? _producto;
        private int _cantidad;
        private decimal _precioCompra;
        private decimal _subtotal;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int CompraId
        {
            get { return _compraId; }
            set { _compraId = value; }
        }

        public Compra? Compra
        {
            get { return _compra; }
            set { _compra = value; }
        }

        public int ProductoId
        {
            get { return _productoId; }
            set { _productoId = value; }
        }

        public Producto? Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }

        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        public decimal PrecioCompra
        {
            get { return _precioCompra; }
            set { _precioCompra = value; }
        }

        public decimal Subtotal
        {
            get { return _subtotal; }
            set { _subtotal = value; }
        }
    }
}
