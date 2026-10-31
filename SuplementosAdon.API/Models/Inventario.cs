
using System;

namespace SuplementosAdon.API.Models
{
    public class Inventario
    {
        private int _id;
        private int _productoId;
        private int _cantidadDisponible;
        private int _stockMinimo;
        private DateTime _fechaActualizacion;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int ProductoId
        {
            get { return _productoId; }
            set { _productoId = value; }
        }
        private Producto? _producto;

        public Producto? Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }
        public int CantidadDisponible
        {
            get { return _cantidadDisponible; }
            set { _cantidadDisponible = value; }
        }

        public int StockMinimo
        {
            get { return _stockMinimo; }
            set { _stockMinimo = value; }
        }

        public DateTime FechaActualizacion
        {
            get { return _fechaActualizacion; }
            set { _fechaActualizacion = value; }
        }
    }
}
