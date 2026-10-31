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
    public class CompraService : ICompraService
    {
        private readonly SuplementosAdonContext _context;

        public CompraService(SuplementosAdonContext context)
        {
            _context = context;
        }



        public async Task RegistrarCompraAsync(CompraDTO compraDTO)
        {
            if (compraDTO == null)
            {
                throw new ArgumentNullException(nameof(compraDTO));
            }

            if (compraDTO.Detalles == null || !compraDTO.Detalles.Any())
            {
                throw new InvalidOperationException(
                    "La compra debe contener al menos un producto.");
            }

            var proveedor = await _context.Proveedores
                .FirstOrDefaultAsync(p => p.Id == compraDTO.ProveedorId);

            if (proveedor == null)
            {
                throw new InvalidOperationException("El proveedor no existe.");
            }

            // Validar todos los detalles antes de modificar el inventario.
            var productosCompra = new List<(Producto Producto, int Cantidad, decimal Precio)>();

            foreach (var detalleDTO in compraDTO.Detalles)
            {
                if (detalleDTO.Cantidad <= 0)
                {
                    throw new InvalidOperationException(
                        "La cantidad de cada producto debe ser mayor que cero.");
                }

                if (detalleDTO.PrecioCompra < 0)
                {
                    throw new InvalidOperationException(
                        "El precio de compra no puede ser negativo.");
                }

                var producto = await _context.Productos
                    .FirstOrDefaultAsync(p => p.Id == detalleDTO.ProductoId);

                if (producto == null)
                {
                    throw new InvalidOperationException(
                        $"El producto con ID {detalleDTO.ProductoId} no existe.");
                }

                productosCompra.Add((
                    producto,
                    detalleDTO.Cantidad,
                    detalleDTO.PrecioCompra));
            }

            // Comprobar que las sumas de cantidades no excedan el límite entero.
            foreach (var grupo in productosCompra.GroupBy(x => x.Producto.Id))
            {
                int cantidadTotal = 0;

                foreach (var item in grupo)
                {
                    cantidadTotal = checked(cantidadTotal + item.Cantidad);
                }

                _ = checked(grupo.First().Producto.Stock + cantidadTotal);
            }

            using var transaccion = await _context.Database.BeginTransactionAsync();

            try
            {
                var compra = new Compra
                {
                    ProveedorId = proveedor.Id,
                    Fecha = DateTime.UtcNow,
                    Total = 0
                };

                decimal totalCompra = 0;

                foreach (var grupo in productosCompra.GroupBy(x => x.Producto.Id))
                {
                    var producto = grupo.First().Producto;
                    int cantidadTotal = grupo.Sum(x => x.Cantidad);

                    producto.Stock = checked(producto.Stock + cantidadTotal);

                    foreach (var item in grupo)
                    {
                        decimal subtotal = item.Cantidad * item.Precio;
                        totalCompra += subtotal;

                        compra.Detalles.Add(new DetalleCompra
                        {
                            ProductoId = producto.Id,
                            Cantidad = item.Cantidad,
                            PrecioCompra = item.Precio,
                            Subtotal = subtotal
                        });
                    }
                }

                compra.Total = totalCompra;

                _context.Compras.Add(compra);

                await _context.SaveChangesAsync();
                await transaccion.CommitAsync();
            }
            catch
            {
                await transaccion.RollbackAsync();
                throw;
            }
        }

    }
}