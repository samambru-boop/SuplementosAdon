using Microsoft.EntityFrameworkCore;
using SuplementosAdon.Desktop.Datos;
using SuplementosAdon.Desktop.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuplementosAdon.Desktop
{
    public partial class FrmProveedores : Form
    {

        public FrmProveedores()
        {
            InitializeComponent();
            CargarProveedores();
        }
        private void CargarProveedores()
        {
            try
            {
                using (var db = new SuplementosAdonContext())
                {
                    var proveedores = db.Proveedores
                    .AsNoTracking()
                    .ToList();


                    dgvProveedores.DataSource = proveedores;
                }

                dgvProveedores.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los proveedores: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void FrmProveedores_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("Debes ingresar el nombre del proveedor.");
                    txtNombre.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEmpresa.Text))
                {
                    MessageBox.Show("Debes ingresar la empresa.");
                    txtEmpresa.Focus();
                    return;
                }

                using (var db = new SuplementosAdonContext())
                {
                    Proveedor proveedor = new Proveedor();

                    proveedor.Nombre = txtNombre.Text.Trim();
                    proveedor.Telefono = txtTelefono.Text.Trim();
                    proveedor.Direccion = txtDireccion.Text.Trim();
                    proveedor.Email = txtEmail.Text.Trim();
                    proveedor.Empresa = txtEmpresa.Text.Trim();
                    proveedor.Activo = chkActivo.Text.Trim().ToLower() == "true";

                    db.Proveedores.Add(proveedor);
                    db.SaveChanges();
                }

                MessageBox.Show(
                    "Proveedor guardado correctamente.",
                    "Correcto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CargarProveedores();

                txtNombre.Clear();
                txtTelefono.Clear();
                txtDireccion.Clear();
                txtEmail.Clear();
                txtEmpresa.Clear();
                chkActivo.Text = "true";
                txtNombre.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al guardar el proveedor: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow fila = dgvProveedores.Rows[e.RowIndex];

            txtNombre.Text = fila.Cells["Nombre"].Value?.ToString() ?? "";
            txtTelefono.Text = fila.Cells["Telefono"].Value?.ToString() ?? "";
            txtDireccion.Text = fila.Cells["Direccion"].Value?.ToString() ?? "";
            txtEmail.Text = fila.Cells["Email"].Value?.ToString() ?? "";
            txtEmpresa.Text = fila.Cells["Empresa"].Value?.ToString() ?? "";
            chkActivo.Text = fila.Cells["Activo"].Value?.ToString() ?? "";
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProveedores.CurrentRow == null)
                {
                    MessageBox.Show("Selecciona un proveedor para editar.");
                    return;
                }

                int id = Convert.ToInt32(
                    dgvProveedores.CurrentRow.Cells["Id"].Value);

                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("Debes ingresar el nombre del proveedor.");
                    txtNombre.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEmpresa.Text))
                {
                    MessageBox.Show("Debes ingresar la empresa.");
                    txtEmpresa.Focus();
                    return;
                }

                using (var db = new SuplementosAdonContext())
                {
                    Proveedor? proveedor = db.Proveedores
                        .FirstOrDefault(p => p.Id == id);

                    if (proveedor == null)
                    {
                        MessageBox.Show("No se encontró el proveedor.");
                        return;
                    }

                    proveedor.Nombre = txtNombre.Text.Trim();
                    proveedor.Telefono = txtTelefono.Text.Trim();
                    proveedor.Direccion = txtDireccion.Text.Trim();
                    proveedor.Email = txtEmail.Text.Trim();
                    proveedor.Empresa = txtEmpresa.Text.Trim();
                    proveedor.Activo = chkActivo.Text.Trim().ToLower() == "true";

                    db.SaveChanges();
                }

                MessageBox.Show(
                    "Proveedor actualizado correctamente.",
                    "Correcto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CargarProveedores();

                txtNombre.Clear();
                txtTelefono.Clear();
                txtDireccion.Clear();
                txtEmail.Clear();
                txtEmpresa.Clear();
                chkActivo.Text = "true";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al actualizar el proveedor: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProveedores.CurrentRow == null)
                {
                    MessageBox.Show("Selecciona un proveedor para eliminar.");
                    return;
                }

                int id = Convert.ToInt32(
                    dgvProveedores.CurrentRow.Cells["Id"].Value);

                string nombre = dgvProveedores.CurrentRow.Cells["Nombre"].Value?.ToString() ?? "";

                DialogResult resultado = MessageBox.Show(
                    "¿Está seguro de que desea eliminar al proveedor:\n\n" + nombre + "?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (resultado != DialogResult.Yes)
                    return;

                using (var db = new SuplementosAdonContext())
                {
                    Proveedor? proveedor = db.Proveedores
                        .FirstOrDefault(p => p.Id == id);

                    if (proveedor == null)
                    {
                        MessageBox.Show("No se encontró el proveedor.");
                        return;
                    }

                    db.Proveedores.Remove(proveedor);
                    db.SaveChanges();
                }

                MessageBox.Show(
                    "Proveedor eliminado correctamente.",
                    "Correcto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CargarProveedores();

                txtNombre.Clear();
                txtTelefono.Clear();
                txtDireccion.Clear();
                txtEmail.Clear();
                txtEmpresa.Clear();
                chkActivo.Text = "true";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al eliminar el proveedor: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string texto = txtBuscar.Text.Trim().ToLower();

                using (var db = new SuplementosAdonContext())
                {
                    var proveedores = db.Proveedores
                        .AsNoTracking()
                        .Where(p =>
                            p.Nombre.ToLower().Contains(texto) ||
                            p.Empresa.ToLower().Contains(texto) ||
                            p.Telefono.ToLower().Contains(texto) ||
                            p.Email.ToLower().Contains(texto))
                        .ToList();

                    dgvProveedores.DataSource = proveedores;
                }

                dgvProveedores.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al buscar proveedores: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            txtEmail.Clear();
            txtEmpresa.Clear();
           
        }
    }
}

