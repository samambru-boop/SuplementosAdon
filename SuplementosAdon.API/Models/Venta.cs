using System;
using System.Collections.Generic;

namespace SuplementosAdon.API.Models
{
    public class Venta
    {
        private int _id;
        private DateTime _fecha = DateTime.UtcNow;
        private int _clienteId;
        private Cliente? _cliente;
        private decimal _total;
        private ICollection<DetalleVenta> _detalles = new List<DetalleVenta>();

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

        public int ClienteId
        {
            get { return _clienteId; }
            set { _clienteId = value; }
        }

        public Cliente? Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }

        public decimal Total
        {
            get { return _total; }
            set { _total = value; }
        }

        public ICollection<DetalleVenta> Detalles
        {
            get { return _detalles; }
            set { _detalles = value; }
        }
    }
}