using Microsoft.EntityFrameworkCore;
using SuplementosAdon.API.Data;
using SuplementosAdon.API.DTOs;
using SuplementosAdon.API.Interfaces;
using SuplementosAdon.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SuplementosAdon.API.Services
{
    public class VentaService : IVentaService
    {
        private readonly SuplementosAdonContext _context;

        public VentaService(SuplementosAdonContext context)
        {
            _context = context;
        }



        public async Task RegistrarVentaAsync(VentaDTO ventaDTO)
        {
            if (ventaDTO == null)
            {
                throw new ArgumentNullException(nameof(ventaDTO));
            }

            if (ventaDTO.Detalles == null || !ventaDTO.Detalles.Any())
            {
                throw new InvalidOperationException(
                    "La venta debe contener al menos un producto.");
            }

            // Validar que exista el cliente.
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == ventaDTO.ClienteId);

            if (cliente == null)
            {
                throw new InvalidOperationException("El cliente no existe.");
            }

            // Validar todos los detalles antes de modificar el inventario.
            var productosVenta = new List<(Producto Producto, int Cantidad)>();

            foreach (var detalleDTO in ventaDTO.Detalles)
            {
                if (detalleDTO.Cantidad <= 0)
                {
                    throw new InvalidOperationException(
                        "La cantidad de cada producto debe ser mayor que cero.");
                }

                var producto = await _context.Productos
                    .FirstOrDefaultAsync(p => p.Id == detalleDTO.ProductoId);

                if (producto == null)
                {
                    throw new InvalidOperationException(
                        $"El producto con ID {detalleDTO.ProductoId} no existe.");
                }

                if (!producto.Activo)
                {
                    throw new InvalidOperationException(
                        $"El producto {producto.Nombre} está inactivo.");
                }

                productosVenta.Add((producto, detalleDTO.Cantidad));
            }

            // Agrupar productos repetidos para validar el stock total necesario.
            foreach (var grupo in productosVenta.GroupBy(x => x.Producto.Id))
            {
                var producto = grupo.First().Producto;
                int cantidadTotal = grupo.Sum(x => x.Cantidad);

                if (producto.Stock < cantidadTotal)
                {
                    throw new InvalidOperationException(
                        $"Stock insuficiente para {producto.Nombre}. " +
                        $"Disponible: {producto.Stock}; solicitado: {cantidadTotal}.");
                }
            }

            // Crear la venta después de validar todos los productos.
            using var transaccion = await _context.Database.BeginTransactionAsync();

            try
            {
                var venta = new Venta
                {
                    ClienteId = cliente.Id,
                    Fecha = DateTime.UtcNow,
                    Total = 0
                };

                decimal totalVenta = 0;

                foreach (var grupo in productosVenta.GroupBy(x => x.Producto.Id))
                {
                    var producto = grupo.First().Producto;
                    int cantidad = grupo.Sum(x => x.Cantidad);

                    // Usar el precio registrado en la base de datos.
                    decimal precioVenta = producto.PrecioVenta;
                    decimal subtotal = cantidad * precioVenta;

                    producto.Stock -= cantidad;
                    totalVenta += subtotal;

                    venta.Detalles.Add(new DetalleVenta
                    {
                        ProductoId = producto.Id,
                        Cantidad = cantidad,
                        PrecioVenta = precioVenta,
                        Subtotal = subtotal
                    });
                }

                venta.Total = totalVenta;

                _context.Ventas.Add(venta);

                await _context.SaveChangesAsync();
                await transaccion.CommitAsync();
            }
            catch
            {
                await transaccion.RollbackAsync();
                throw;
            }
        }



        // Obtener historial de ventas
        public async Task<List<VentaRespuestaDTO>> ObtenerVentasAsync()

        {
          
            var ventas = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Detalles)
                    .ThenInclude(d => d.Producto)
                .ToListAsync();

            return ventas.Select(v => new VentaRespuestaDTO
            {
                Id = v.Id,
                Fecha = v.Fecha,
                Cliente = v.Cliente != null
                    ? $"{v.Cliente.Nombre} {v.Cliente.Apellido}"
                    : "Sin cliente",
                Total = v.Total,

                Detalles = v.Detalles.Select(d => new DetalleVentaRespuestaDTO
                {
                    Producto = d.Producto != null
                        ? d.Producto.Nombre
                        : "Sin producto",

                    Cantidad = d.Cantidad,
                    PrecioVenta = d.PrecioVenta,
                    Subtotal = d.Subtotal

                }).ToList()

            }).ToList();
        }
    }
}
