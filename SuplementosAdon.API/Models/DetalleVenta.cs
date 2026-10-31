using System;

namespace SuplementosAdon.API.Models
{
    public class DetalleVenta
    {
        private int _id;
        private int _ventaId;
        private Venta? _venta;
        private int _productoId;
        private Producto? _producto;
        private int _cantidad;
        private decimal _precioVenta;
        private decimal _subtotal;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int VentaId
        {
            get { return _ventaId; }
            set { _ventaId = value; }
        }

        public Venta? Venta
        {
            get { return _venta; }
            set { _venta = value; }
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

        public decimal PrecioVenta
        {
            get { return _precioVenta; }
            set { _precioVenta = value; }
        }

        public decimal Subtotal
        {
            get { return _subtotal; }
            set { _subtotal = value; }
        }
    }
}