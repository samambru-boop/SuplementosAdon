namespace SuplementosAdon.API.DTOs
{
    public class DashboardDTO
    {
        public int TotalProductos { get; set; }

        public int TotalClientes { get; set; }

        public int TotalProveedores { get; set; }

        public int TotalCompras { get; set; }

        public int TotalVentas { get; set; }

        public decimal TotalVendido { get; set; }

        public decimal TotalComprado { get; set; }

        public int ProductosStockBajo { get; set; }
    }
}