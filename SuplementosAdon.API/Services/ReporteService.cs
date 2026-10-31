using Microsoft.EntityFrameworkCore;
using SuplementosAdon.API.Data;
using SuplementosAdon.API.DTOs;
using SuplementosAdon.API.Interfaces;

namespace SuplementosAdon.API.Services
{
    public class ReporteService : IReporteService
    {
        private readonly SuplementosAdonContext _context;

        public ReporteService(SuplementosAdonContext context)
        {
            _context = context;
        }


        public async Task<ReporteVentasDTO> ObtenerReporteVentasAsync(
            DateTime fechaInicio,
            DateTime fechaFin)
        {

            var ventas = await _context.Ventas
                .Include(v => v.Detalles)
                .Where(v => v.Fecha >= fechaInicio &&
                            v.Fecha <= fechaFin)
                .ToListAsync();


            decimal totalVendido = ventas.Sum(v => v.Total);


            decimal ganancia = 0;


            foreach (var venta in ventas)
            {
                foreach (var detalle in venta.Detalles)
                {
                    var producto = await _context.Productos
                        .FirstOrDefaultAsync(p =>
                            p.Id == detalle.ProductoId);


                    if (producto != null)
                    {
                        ganancia +=
                            (detalle.PrecioVenta - producto.PrecioCompra)
                            * detalle.Cantidad;
                    }
                }
            }


            return new ReporteVentasDTO
            {
                CantidadVentas = ventas.Count,

                TotalVendido = totalVendido,

                GananciaEstimada = ganancia,

                FechaInicio = fechaInicio,

                FechaFin = fechaFin
            };
        }
        public async Task<List<ReporteStockDTO>> ObtenerProductosStockBajoAsync()
        {
            return await _context.Productos
                .Where(p => p.Stock <= p.StockMinimo)
                .OrderBy(p => p.Stock)
                .Select(p => new ReporteStockDTO
                {
                    Id = p.Id,
                    Codigo = p.Codigo,
                    Producto = p.Nombre,
                    Stock = p.Stock,
                    StockMinimo = p.StockMinimo
                })
                .ToListAsync();
        }
        public async Task<List<ReporteVencimientoDTO>> ObtenerProductosProximosAVencerAsync(int dias)
        {
            var hoy = DateTime.UtcNow.Date;
            var fechaLimite = hoy.AddDays(dias);

            var productos = await _context.Productos
                .Where(p => p.FechaVencimiento.HasValue &&
                            p.FechaVencimiento.Value.Date >= hoy &&
                            p.FechaVencimiento.Value.Date <= fechaLimite)
                .OrderBy(p => p.FechaVencimiento)
                .ToListAsync();

            return productos.Select(p => new ReporteVencimientoDTO
            {
                Id = p.Id,
                Codigo = p.Codigo,
                Producto = p.Nombre,
                FechaVencimiento = p.FechaVencimiento!.Value,
                DiasRestantes = (p.FechaVencimiento.Value.Date - hoy).Days
            }).ToList();
        }
        public async Task<DashboardDTO> ObtenerDashboardAsync()
        {
            var dashboard = new DashboardDTO
            {
                TotalProductos = await _context.Productos.CountAsync(),

                TotalClientes = await _context.Clientes.CountAsync(),

                TotalProveedores = await _context.Proveedores.CountAsync(),

                TotalCompras = await _context.Compras.CountAsync(),

                TotalVentas = await _context.Ventas.CountAsync(),

                TotalComprado = await _context.Compras.SumAsync(c => (decimal?)c.Total) ?? 0,

                TotalVendido = await _context.Ventas.SumAsync(v => (decimal?)v.Total) ?? 0,

                ProductosStockBajo = await _context.Productos
                    .CountAsync(p => p.Stock <= p.StockMinimo)
            };

            return dashboard;
        }
    }
}