using System;
using System.Windows.Forms;
using SuplementosAdon.Desktop.Datos;
using SuplementosAdon.Desktop.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq;
using SuplementosAdon.Desktop.Servicios;
namespace SuplementosAdon.Desktop
{
    public partial class FrmProductos : Form
    {
        private int productoSeleccionadoId = 0;
        private readonly ProductoService _productoService =
    new ProductoService();

        private readonly CategoriaService _categoriaService =
        new CategoriaService();

        private readonly MarcaService _marcaService =
    new MarcaService();
        public FrmProductos()
        {
            InitializeComponent();
            CargarProductos();
            CargarMarcas();
            CargarCategorias();
            
        }

        private void CargarMarcas()
        {
            try
            {
                cmbMarca.DataSource = _marcaService.ObtenerTodas();
                cmbMarca.DisplayMember = "Nombre";
                cmbMarca.ValueMember = "Id";
                cmbMarca.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar las marcas:\n" + ex.Message,
                    "Suplementos Adon",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ProbarConexion()
        {
            try
            {
                using (var db = new SuplementosAdonContext())
                {
                    if (db.Database.CanConnect())
                    {
                        MessageBox.Show(
                            "Conexión exitosa con PostgreSQL",
                            "Suplementos Adon",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se pudo conectar con PostgreSQL",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CargarProductos()
        {
            try
            {
                dgvProductos.DataSource = _productoService.ObtenerTodos();
                dgvProductos.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar productos:\n" + ex.Message,
                    "Suplementos Adon",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void CargarCategorias()
        {
            try
            {
                cmbCategoria.DataSource = _categoriaService.ObtenerTodas();
                cmbCategoria.DisplayMember = "Nombre";
                cmbCategoria.ValueMember = "Id";
                cmbCategoria.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar las categorías:\n" + ex.Message,
                    "Suplementos Adon",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }




        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El código y el nombre son obligatorios.");
                return;
            }

            if (cmbCategoria.SelectedValue == null ||
                cmbMarca.SelectedValue == null)
            {
                MessageBox.Show("Selecciona una categoría y una marca.");
                return;
            }

            if (!decimal.TryParse(txtPrecioCompra.Text, out decimal precioCompra) ||
                !decimal.TryParse(txtPrecioVenta.Text, out decimal precioVenta))
            {
                MessageBox.Show("Introduce precios válidos.");
                return;
            }

            if (!int.TryParse(txtStock.Text, out int stock) ||
                !int.TryParse(txtStockMinimo.Text, out int stockMinimo))
            {
                MessageBox.Show("Introduce cantidades enteras válidas.");
                return;
            }

            try
            {
                bool esNuevo = productoSeleccionadoId == 0;
                Producto producto;

                if (esNuevo)
                {
                    producto = new Producto
                    {
                        FechaRegistro = DateTime.UtcNow,
                        Activo = true
                    };
                }
                else
                {
                    var productoEncontrado =
                        _productoService.ObtenerPorId(productoSeleccionadoId);

                    if (productoEncontrado == null)
                    {
                        MessageBox.Show("No se encontró el producto.");
                        return;
                    }

                    producto = productoEncontrado;
                }

                producto.Codigo = txtCodigo.Text.Trim();
                producto.Nombre = txtNombre.Text.Trim();
                producto.PrecioCompra = precioCompra;
                producto.PrecioVenta = precioVenta;
                producto.Stock = stock;
                producto.StockMinimo = stockMinimo;
                producto.CodigoBarras = txtCodigoBarras.Text.Trim();
                producto.Descripcion = txtDescripcion.Text.Trim();
                producto.CategoriaId = Convert.ToInt32(cmbCategoria.SelectedValue);
                producto.MarcaId = Convert.ToInt32(cmbMarca.SelectedValue);

                producto.FechaVencimiento = dtpFechaVencimiento.Checked
                    ? DateTime.SpecifyKind(dtpFechaVencimiento.Value.Date, DateTimeKind.Utc)
                    : null;

                _productoService.Guardar(producto);

                MessageBox.Show(
                    esNuevo ? "Producto guardado correctamente."
                            : "Producto actualizado correctamente.",
                    "Suplementos Adon",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                productoSeleccionadoId = 0;
                LimpiarCampos();
                CargarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo guardar el producto:\n" + ex.Message,
                    "Suplementos Adon",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }



        private void LimpiarCampos()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtPrecioCompra.Clear();
            txtPrecioVenta.Clear();
            txtStock.Clear();
            txtStockMinimo.Clear();
            txtCodigoBarras.Clear();
            txtDescripcion.Clear();

            dtpFechaVencimiento.Value = DateTime.Today;

            txtCodigo.Focus();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {


        }

        private void dtpFechaVencimiento_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

            if (fila.IsNewRow)
                return;

            if (fila.Cells["Id"].Value == null ||
                string.IsNullOrWhiteSpace(fila.Cells["Id"].Value.ToString()))
            {
                MessageBox.Show("El producto seleccionado no tiene un ID válido.");
                return;
            }

            if (!int.TryParse(fila.Cells["Id"].Value.ToString(), out productoSeleccionadoId))
            {
                MessageBox.Show("El ID del producto no tiene un formato válido.");
                return;
            }

            txtCodigo.Text = fila.Cells["Codigo"].Value?.ToString() ?? "";
            txtNombre.Text = fila.Cells["Nombre"].Value?.ToString() ?? "";
            txtPrecioCompra.Text = fila.Cells["PrecioCompra"].Value?.ToString() ?? "";
            txtPrecioVenta.Text = fila.Cells["PrecioVenta"].Value?.ToString() ?? "";
            txtStock.Text = fila.Cells["Stock"].Value?.ToString() ?? "";
            txtStockMinimo.Text = fila.Cells["StockMinimo"].Value?.ToString() ?? "";
            txtCodigoBarras.Text = fila.Cells["CodigoBarras"].Value?.ToString() ?? "";
            txtDescripcion.Text = fila.Cells["Descripcion"].Value?.ToString() ?? "";
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (productoSeleccionadoId == 0)
            {
                MessageBox.Show("Selecciona un producto para actualizar.");
                return;
            }

            try
            {
                var producto = _productoService.ObtenerPorId(
                    productoSeleccionadoId);

                if (producto == null)
                {
                    MessageBox.Show("No se encontró el producto.");
                    return;
                }

                producto.Codigo = txtCodigo.Text.Trim();
                producto.Nombre = txtNombre.Text.Trim();

                if (!decimal.TryParse(txtPrecioCompra.Text, out decimal precioCompra) ||
                    !decimal.TryParse(txtPrecioVenta.Text, out decimal precioVenta))
                {
                    MessageBox.Show("Introduce precios válidos.");
                    return;
                }

                if (!int.TryParse(txtStock.Text, out int stock) ||
                    !int.TryParse(txtStockMinimo.Text, out int stockMinimo))
                {
                    MessageBox.Show("Introduce cantidades válidas para el stock.");
                    return;
                }

                if (cmbCategoria.SelectedValue == null ||
                    !int.TryParse(cmbCategoria.SelectedValue.ToString(), out int categoriaId))
                {
                    MessageBox.Show("Selecciona una categoría.");
                    return;
                }

                if (cmbMarca.SelectedValue == null ||
                    !int.TryParse(cmbMarca.SelectedValue.ToString(), out int marcaId))
                {
                    MessageBox.Show("Selecciona una marca.");
                    return;
                }

                producto.PrecioCompra = precioCompra;
                producto.PrecioVenta = precioVenta;
                producto.Stock = stock;
                producto.StockMinimo = stockMinimo;
                producto.CodigoBarras = txtCodigoBarras.Text.Trim();
                producto.Descripcion = txtDescripcion.Text.Trim();
                producto.CategoriaId = categoriaId;
                producto.MarcaId = marcaId;

                producto.FechaVencimiento = dtpFechaVencimiento.Checked
                    ? DateTime.SpecifyKind(
                        dtpFechaVencimiento.Value,
                        DateTimeKind.Utc)
                    : null;

                _productoService.Guardar(producto);

                MessageBox.Show(
                    "Producto actualizado correctamente.",
                    "Suplementos Adon",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LimpiarCampos();
                CargarProductos();
                productoSeleccionadoId = 0;
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;

                if (ex.InnerException != null)
                    mensaje += "\n\nDetalle:\n" + ex.InnerException.Message;

                MessageBox.Show(
                    mensaje,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (productoSeleccionadoId == 0)
            {
                MessageBox.Show("Selecciona un producto de la tabla antes de editar.");
                return;
            }

            try
            {
                using (var db = new SuplementosAdonContext())
                {
                    var producto = db.Productos.Find(productoSeleccionadoId);

                    if (producto == null)
                    {
                        MessageBox.Show("No se encontró el producto.");
                        return;
                    }

                    producto.Codigo = txtCodigo.Text;
                    producto.Nombre = txtNombre.Text;
                    producto.PrecioCompra = decimal.Parse(txtPrecioCompra.Text);
                    producto.PrecioVenta = decimal.Parse(txtPrecioVenta.Text);
                    producto.Stock = int.Parse(txtStock.Text);
                    producto.StockMinimo = int.Parse(txtStockMinimo.Text);
                    producto.CodigoBarras = txtCodigoBarras.Text;
                    producto.Descripcion = txtDescripcion.Text;

                    db.SaveChanges();

                    MessageBox.Show("Producto actualizado correctamente.");

                    CargarProductos();
                    LimpiarCampos();

                    productoSeleccionadoId = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el producto:\n" + ex.Message);
            }
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (productoSeleccionadoId == 0)
            {
                MessageBox.Show(
                    "Selecciona un producto de la tabla antes de eliminar.");
                return;
            }

            DialogResult resultado = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar este producto?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado != DialogResult.Yes)
                return;

            try
            {
                _productoService.Eliminar(productoSeleccionadoId);

                MessageBox.Show("Producto eliminado correctamente.");

                CargarProductos();
                LimpiarCampos();

                productoSeleccionadoId = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al eliminar el producto:\n" + ex.Message);
            }
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            productoSeleccionadoId = 0;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {

                string texto = txtBuscar.Text.Trim();

                dgvProductos.DataSource = _productoService.Buscar(texto);
                dgvProductos.ClearSelection();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar productos:\n" + ex.Message);
            }
        }
    }
}