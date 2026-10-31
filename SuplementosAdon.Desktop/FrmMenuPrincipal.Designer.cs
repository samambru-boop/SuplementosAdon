namespace SuplementosAdon.Desktop
{
    partial class FrmMenuPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Panel panelMenu;
        private Panel panelContenido;
        private Button btnInicio;
        private Button btnProductos;
        private Button btnClientes;
        private Button btnVentas;
        private Button btnUsuarios;
        private Button btnReportes;
        private Button btnSalir;

        private PictureBox pictureLogo;
        private Label lblTitulo;
        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelMenu = new Panel();
            btnMarcas = new Button();
            button1 = new Button();
            proveedoresToolStripMenuItem = new Button();
            btnInicio = new Button();
            btnProductos = new Button();
            btnClientes = new Button();
            btnVentas = new Button();
            btnUsuarios = new Button();
            btnReportes = new Button();
            btnSalir = new Button();
            lblTitulo = new Label();
            pictureLogo = new PictureBox();
            panelContenido = new Panel();
            panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureLogo).BeginInit();
            panelContenido.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.DarkGreen;
            panelMenu.Controls.Add(btnMarcas);
            panelMenu.Controls.Add(button1);
            panelMenu.Controls.Add(proveedoresToolStripMenuItem);
            panelMenu.Controls.Add(btnInicio);
            panelMenu.Controls.Add(btnProductos);
            panelMenu.Controls.Add(btnClientes);
            panelMenu.Controls.Add(btnVentas);
            panelMenu.Controls.Add(btnUsuarios);
            panelMenu.Controls.Add(btnReportes);
            panelMenu.Controls.Add(btnSalir);
            panelMenu.Controls.Add(lblTitulo);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(284, 700);
            panelMenu.TabIndex = 1;
            panelMenu.Paint += panelMenu_Paint;
            // 
            // btnMarcas
            // 
            btnMarcas.Location = new Point(15, 254);
            btnMarcas.Name = "btnMarcas";
            btnMarcas.Size = new Size(220, 45);
            btnMarcas.TabIndex = 10;
            btnMarcas.Text = "Marcas";
            btnMarcas.Click += btnMarcas_Click;
            // 
            // button1
            // 
            button1.Location = new Point(15, 203);
            button1.Name = "button1";
            button1.Size = new Size(220, 45);
            button1.TabIndex = 9;
            button1.Text = "categoria";
            button1.Click += button1_Click;
            // 
            // proveedoresToolStripMenuItem
            // 
            proveedoresToolStripMenuItem.Location = new Point(15, 308);
            proveedoresToolStripMenuItem.Name = "proveedoresToolStripMenuItem";
            proveedoresToolStripMenuItem.Size = new Size(220, 45);
            proveedoresToolStripMenuItem.TabIndex = 8;
            proveedoresToolStripMenuItem.Text = "Proveedores";
            proveedoresToolStripMenuItem.Click += proveedoresToolStripMenuItem_Click;
            // 
            // btnInicio
            // 
            btnInicio.Location = new Point(15, 50);
            btnInicio.Name = "btnInicio";
            btnInicio.Size = new Size(220, 45);
            btnInicio.TabIndex = 0;
            btnInicio.Text = "Inicio";
            // 
            // btnProductos
            // 
            btnProductos.Location = new Point(15, 101);
            btnProductos.Name = "btnProductos";
            btnProductos.Size = new Size(220, 45);
            btnProductos.TabIndex = 1;
            btnProductos.Text = "Productos";
            btnProductos.Click += btnProductos_Click;
            // 
            // btnClientes
            // 
            btnClientes.Location = new Point(15, 152);
            btnClientes.Name = "btnClientes";
            btnClientes.Size = new Size(220, 45);
            btnClientes.TabIndex = 2;
            btnClientes.Text = "Clientes";
            // 
            // btnVentas
            // 
            btnVentas.Location = new Point(15, 359);
            btnVentas.Name = "btnVentas";
            btnVentas.Size = new Size(220, 45);
            btnVentas.TabIndex = 3;
            btnVentas.Text = "Ventas";
            // 
            // btnUsuarios
            // 
            btnUsuarios.Location = new Point(15, 461);
            btnUsuarios.Name = "btnUsuarios";
            btnUsuarios.Size = new Size(220, 45);
            btnUsuarios.TabIndex = 4;
            btnUsuarios.Text = "Usuarios";
            // 
            // btnReportes
            // 
            btnReportes.Location = new Point(15, 410);
            btnReportes.Name = "btnReportes";
            btnReportes.Size = new Size(220, 45);
            btnReportes.TabIndex = 5;
            btnReportes.Text = "Reportes";
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(15, 512);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(220, 45);
            btnSalir.TabIndex = 6;
            btnSalir.Text = "Salir";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(20, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(208, 25);
            lblTitulo.TabIndex = 7;
            lblTitulo.Text = "SUPLEMENTOS ADON";
            // 
            // pictureLogo
            // 
            pictureLogo.Location = new Point(6, 20);
            pictureLogo.Name = "pictureLogo";
            pictureLogo.Size = new Size(100, 100);
            pictureLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureLogo.TabIndex = 8;
            pictureLogo.TabStop = false;
            // 
            // panelContenido
            // 
            panelContenido.BackColor = Color.White;
            panelContenido.Controls.Add(pictureLogo);
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.Location = new Point(284, 0);
            panelContenido.Name = "panelContenido";
            panelContenido.Size = new Size(916, 700);
            panelContenido.TabIndex = 0;
            // 
            // FrmMenuPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 700);
            Controls.Add(panelContenido);
            Controls.Add(panelMenu);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmMenuPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SGV - Suplementos Adon";
            panelMenu.ResumeLayout(false);
            panelMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureLogo).EndInit();
            panelContenido.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button proveedoresToolStripMenuItem;
        private Button button1;
        private Button btnMarcas;
    }
}
