using System;
using System.Windows.Forms;

namespace Jardines2023.Windows
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void PaisesButton_Click(object sender, EventArgs e)
        {
            frmPaises frm=new frmPaises();
            frm.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CategoriasButton_Click(object sender, EventArgs e)
        {
            frmCategorias frm=new frmCategorias();
            frm.ShowDialog();
        }

        private void CiudadesButton_Click(object sender, EventArgs e)
        {
            frmCiudades frm=new frmCiudades();
            frm.ShowDialog();
        }

        private void ClientesButton_Click(object sender, EventArgs e)
        {
            frmClientes frm=new frmClientes();
            frm.ShowDialog(this);
        }

        private void ProveedoresButton_Click(object sender, EventArgs e)
        {
            frmProveedores frm=new frmProveedores();
            frm.ShowDialog(this);
        }

        private void ProductosButton_Click(object sender, EventArgs e)
        {
            frmProductos frm=new frmProductos();
            frm.ShowDialog(this);
        }

        private void VentasButton_Click(object sender, EventArgs e)
        {
            frmVentas frm = new frmVentas();
            frm.ShowDialog(this);
        }
    }
}
