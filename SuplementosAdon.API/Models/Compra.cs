
using System;
using System.Collections.Generic;

namespace SuplementosAdon.API.Models
{
    public class Compra
    {
        private int _id;
        private DateTime _fecha = DateTime.UtcNow;
        private int _proveedorId;
        private Proveedor? _proveedor;
        private decimal _total;
        private ICollection<DetalleCompra> _detalles = new List<DetalleCompra>();

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public int ProveedorId
        {
            get { return _proveedorId; }
            set { _proveedorId = value; }
        }

        public Proveedor? Proveedor
        {
            get { return _proveedor; }
            set { _proveedor = value; }
        }

        public decimal Total
        {
            get { return _total; }
            set { _total = value; }
        }

        public ICollection<DetalleCompra> Detalles
        {
            get { return _detalles; }
            set { _detalles = value; }
        }
    }
}
