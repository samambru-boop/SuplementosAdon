
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SuplementosAdon.Desktop.Datos;
using SuplementosAdon.Desktop.Modelos;

namespace SuplementosAdon.Desktop.Servicios
{
    public class ProductoService
    {
        public List<Producto> ObtenerTodos()
        {
            using (var db = new SuplementosAdonContext())
            {
                return db.Productos
                    .AsNoTracking()
                    .OrderBy(p => p.Nombre)
                    .ToList();
            }
        }

        public List<Producto> Buscar(string texto)
        {
            texto = (texto ?? "").Trim();

            using (var db = new SuplementosAdonContext())
            {
                return db.Productos
                    .AsNoTracking()
                    .Where(p =>
                        p.Codigo.Contains(texto) ||
                        p.Nombre.Contains(texto) ||
                        (p.CodigoBarras ?? "").Contains(texto))
                    .OrderBy(p => p.Nombre)
                    .ToList();
            }
        }

        public Producto? ObtenerPorId(int id)
        {
            using (var db = new SuplementosAdonContext())
            {
                return db.Productos
                    .AsNoTracking()
                    .FirstOrDefault(p => p.Id == id);
            }
        }

        public void Guardar(Producto producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));

            ValidarProducto(producto);

            using (var db = new SuplementosAdonContext())
            {
                bool codigoExiste = db.Productos.Any(p =>
                    p.Codigo == producto.Codigo &&
                    p.Id != producto.Id);

                if (codigoExiste)
                    throw new InvalidOperationException(
                        "Ya existe un producto con ese código.");

                if (producto.Id == 0)
                {
                    db.Productos.Add(producto);
                }
                else
                {
                    var existente = db.Productos
                        .FirstOrDefault(p => p.Id == producto.Id);

                    if (existente == null)
                        throw new InvalidOperationException(
                            "No se encontró el producto que deseas actualizar.");

                    existente.Codigo = producto.Codigo.Trim();
                    existente.Nombre = producto.Nombre.Trim();
                    existente.PrecioCompra = producto.PrecioCompra;
                    existente.PrecioVenta = producto.PrecioVenta;
                    existente.Stock = producto.Stock;
                    existente.StockMinimo = producto.StockMinimo;
                    existente.CodigoBarras = producto.CodigoBarras;
                    existente.Descripcion = producto.Descripcion;
                    existente.CategoriaId = producto.CategoriaId;
                    existente.MarcaId = producto.MarcaId;
                    existente.FechaVencimiento = producto.FechaVencimiento;
                    existente.Activo = producto.Activo;
                }

                db.SaveChanges();
            }
        }

        public void Eliminar(int id)
        {
            using (var db = new SuplementosAdonContext())
            {
                var producto = db.Productos.Find(id);

                if (producto == null)
                    throw new InvalidOperationException(
                        "No se encontró el producto.");

                db.Productos.Remove(producto);
                db.SaveChanges();
            }
        }

        private static void ValidarProducto(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Codigo))
                throw new ArgumentException(
                    "El código del producto es obligatorio.");

            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new ArgumentException(
                    "El nombre del producto es obligatorio.");

            if (producto.PrecioCompra < 0 ||
                producto.PrecioVenta < 0)
                throw new ArgumentException(
                    "Los precios no pueden ser negativos.");

            if (producto.PrecioVenta < producto.PrecioCompra)
                throw new ArgumentException(
                    "El precio de venta no puede ser menor que el de compra.");

            if (producto.Stock < 0 || producto.StockMinimo < 0)
                throw new ArgumentException(
                    "El stock y el stock mínimo no pueden ser negativos.");

            if (producto.CategoriaId <= 0)
                throw new ArgumentException(
                    "Debes seleccionar una categoría.");
        }
    }
}
