using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuplementosAdon.Desktop.Datos;
using SuplementosAdon.Desktop.Modelos;

namespace SuplementosAdon.Desktop
{
    public partial class FrmMarcas : Form
    {
        private int marcaSeleccionadaId = 0;
        public FrmMarcas()
        {
            InitializeComponent();
            CargarMarcas();
        }

        private void CargarMarcas()
        {
            using (var db = new SuplementosAdonContext())
            {
                dgvMarcas.DataSource = db.Marcas.ToList();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Debes ingresar el nombre de la marca.");
                txtNombre.Focus();
                return;
            }

            using (var db = new SuplementosAdonContext())
            {
                Marca marca = new Marca();

                marca.Nombre = txtNombre.Text;
                marca.Descripcion = txtDescripcion.Text;
                marca.Activo = true;

                db.Marcas.Add(marca);
                db.SaveChanges();
            }

            MessageBox.Show("Marca guardada correctamente.");

            txtNombre.Clear();
            txtDescripcion.Clear();

            CargarMarcas();

        }
        private void FrmMarcas_Load(object sender, EventArgs e)
        {
        }

        private void dgvMarcas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            object? valorId = dgvMarcas.Rows[e.RowIndex].Cells["Id"].Value;

            if (valorId == null || valorId == DBNull.Value)
                return;

            marcaSeleccionadaId = Convert.ToInt32(valorId);

            txtNombre.Text = dgvMarcas.Rows[e.RowIndex].Cells["Nombre"].Value?.ToString() ?? "";

            txtDescripcion.Text = dgvMarcas.Rows[e.RowIndex].Cells["Descripcion"].Value?.ToString() ?? "";
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (marcaSeleccionadaId == 0)
            {
                MessageBox.Show("Selecciona una marca para editar.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Debes ingresar el nombre de la marca.");
                txtNombre.Focus();
                return;
            }

            using (var db = new SuplementosAdonContext())
            {
                Marca? marca = db.Marcas.FirstOrDefault(
                    m => m.Id == marcaSeleccionadaId);

                if (marca == null)
                {
                    MessageBox.Show("No se encontró la marca.");
                    return;
                }

                marca.Nombre = txtNombre.Text;
                marca.Descripcion = txtDescripcion.Text;

                db.SaveChanges();
            }

            MessageBox.Show("Marca actualizada correctamente.");

            txtNombre.Clear();
            txtDescripcion.Clear();
            marcaSeleccionadaId = 0;

            CargarMarcas();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (marcaSeleccionadaId == 0)
            {
                MessageBox.Show("Selecciona una marca para eliminar.");
                return;
            }

            DialogResult resultado = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar esta marca?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado != DialogResult.Yes)
            {
                return;
            }

            using (var db = new SuplementosAdonContext())
            {
                Marca? marca = db.Marcas.FirstOrDefault(
                    m => m.Id == marcaSeleccionadaId);

                if (marca == null)
                {
                    MessageBox.Show("No se encontró la marca.");
                    return;
                }

                db.Marcas.Remove(marca);
                db.SaveChanges();
            }

            MessageBox.Show("Marca eliminada correctamente.");

            txtNombre.Clear();
            txtDescripcion.Clear();
            marcaSeleccionadaId = 0;

            CargarMarcas();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string texto = txtBuscar.Text.Trim();

            using (var db = new SuplementosAdonContext())
            {
                dgvMarcas.DataSource = db.Marcas
                    .Where(m => m.Nombre.Contains(texto))
                    .ToList();
            }
        }
    }

}
