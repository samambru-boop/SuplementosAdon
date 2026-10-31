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
    public partial class FrmCategorias : Form
    {
        private int categoriaSeleccionadaId = 0;
        public FrmCategorias()
        {
            InitializeComponent();
            CargarCategorias();
        }

        private void CargarCategorias()
        {
            using (var db = new SuplementosAdonContext())
            {
                dgvCategorias.DataSource = db.Categorias.ToList();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Entró al botón Guardar");

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Debes ingresar el nombre de la categoría.");
                txtNombre.Focus();
                return;
            }

            using (var db = new SuplementosAdonContext())
            {
                Categoria categoria = new Categoria();

                categoria.Nombre = txtNombre.Text;

                db.Categorias.Add(categoria);
                db.SaveChanges();
            }

            MessageBox.Show("Categoría guardada correctamente.");

            txtNombre.Clear();

            CargarCategorias();
        }

        private void dgvCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvCategorias.Rows[e.RowIndex];

                categoriaSeleccionadaId = Convert.ToInt32(fila.Cells["Id"].Value);
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (categoriaSeleccionadaId == 0)
            {
                MessageBox.Show("Selecciona una categoría de la tabla.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Debes ingresar el nombre de la categoría.");
                txtNombre.Focus();
                return;
            }

            using (var db = new SuplementosAdonContext())
            {
                Categoria categoria = db.Categorias
                    .FirstOrDefault(c => c.Id == categoriaSeleccionadaId);

                if (categoria == null)
                {
                    MessageBox.Show("No se encontró la categoría.");
                    return;
                }

                categoria.Nombre = txtNombre.Text;

                db.SaveChanges();
            }

            MessageBox.Show("Categoría actualizada correctamente.");

            txtNombre.Clear();
            categoriaSeleccionadaId = 0;

            CargarCategorias();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (categoriaSeleccionadaId == 0)
            {
                MessageBox.Show("Selecciona una categoría de la tabla.");
                return;
            }

            DialogResult resultado = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar esta categoría?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado != DialogResult.Yes)
            {
                return;
            }

            using (var db = new SuplementosAdonContext())
            {
                Categoria categoria = db.Categorias
                    .FirstOrDefault(c => c.Id == categoriaSeleccionadaId);

                if (categoria == null)
                {
                    MessageBox.Show("No se encontró la categoría.");
                    return;
                }

                db.Categorias.Remove(categoria);
                db.SaveChanges();
            }

            MessageBox.Show("Categoría eliminada correctamente.");

            txtNombre.Clear();
            categoriaSeleccionadaId = 0;

            CargarCategorias();
        }
    }
}
