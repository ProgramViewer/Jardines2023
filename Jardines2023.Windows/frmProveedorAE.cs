using Jardines2023.Entidades.Entidades;
using Jardines2023.Servicios.Interfaces;
using Jardines2023.Windows.Helpers;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Jardines2023.Windows
{
    public partial class frmProveedorAE : Form
    {
        private readonly IServiciosProveedores _servicios;
        public frmProveedorAE(IServiciosProveedores servicio)
        {
            InitializeComponent();
            _servicios = servicio;
        }
        private Proveedor proveedor;
        public Proveedor GetProveedor()
        {
            return proveedor;
        }

        public void SetProveedor(Proveedor proveedor)
        {
            this.proveedor = proveedor;
        }
       
        private bool esEdicion = false;
        
       
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CombosHelper.CargarComboPaises(ref cboPaises);
            if (proveedor != null)
            {
                esEdicion = true;
                txtProveedor.Text = proveedor.NombreProveedor;
                txtDireccion.Text = proveedor.Direccion;
                txtCodPostal.Text = proveedor.CodigoPostal;
                txtFijo.Text = proveedor.TelefonoFijo;
                txtCelular.Text = proveedor.TelefonoMovil;
                cboPaises.SelectedValue = proveedor.PaisId;
                CombosHelper.CargarComboCiudades(ref cboCiudades, proveedor.PaisId);
                cboCiudades.SelectedValue = proveedor.CiudadId;
                txtEmail.Text = proveedor.Email;
            }
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (proveedor == null)
                {
                    proveedor = new Proveedor();
                }
                proveedor.ProveedorId = proveedor.ProveedorId;
                proveedor.NombreProveedor = txtProveedor.Text;
                proveedor.Direccion = txtDireccion.Text;
                proveedor.CodigoPostal = txtCodPostal.Text;
                proveedor.TelefonoFijo = txtFijo.Text;
                proveedor.TelefonoMovil = txtCelular.Text;
                proveedor.Pais = (Pais)cboPaises.SelectedItem;
                proveedor.PaisId = (int)cboPaises.SelectedValue;
                proveedor.Ciudad = (Ciudad)cboCiudades.SelectedItem;
                proveedor.CiudadId = (int)cboCiudades.SelectedValue;
                proveedor.Email = txtEmail.Text;

                try
                {

                    if (!_servicios.Existe(proveedor))
                    {
                        _servicios.Guardar(proveedor);

                        if (!esEdicion)
                        {
                            MessageBox.Show("Registro agregado",
                        "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DialogResult dr = MessageBox.Show("¿Desea agregar otro registro?",
                                "Pregunta",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2);
                            if (dr == DialogResult.No)
                            {
                                DialogResult = DialogResult.OK;

                            }
                            else
                            {
                                proveedor = null;
                                InicializarControles();

                            }

                        }
                        else
                        {
                            MessageBox.Show("Registro editado", "Mensaje",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DialogResult = DialogResult.OK;

                        }
                    }
                    else
                    {
                        MessageBox.Show("Registro duplicado",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        proveedor = null;
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message,
        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }

        private void InicializarControles()
        {
            txtCelular.Clear();
            txtFijo.Clear();
            txtProveedor.Clear();
            txtCodPostal.Clear();
            txtDireccion.Clear();
            txtEmail.Clear();
            //cboCiudades.Items.Clear();
            cboPaises.SelectedIndex = 0;
            txtProveedor.Focus();
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(txtProveedor.Text))
            {
                valido = false;
                errorProvider1.SetError(txtProveedor, "Los nombres son requeridos");
            }
            if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                valido = false;
                errorProvider1.SetError(txtDireccion, "La dirección es requerida");
            }
            if (string.IsNullOrEmpty(txtCodPostal.Text))
            {
                valido = false;
                errorProvider1.SetError(txtCodPostal, "El CP es requerido");
            }
            if (cboPaises.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(cboPaises, "Debe seleccionar un país");
            }
            if (cboCiudades.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(cboCiudades, "Debe seleccionar una ciudad");
            }
            return valido;
        }


        private void cboPaises_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPaises.SelectedIndex > 0)
            {
                var pais = (Pais)cboPaises.SelectedItem;
                CombosHelper.CargarComboCiudades(ref cboCiudades, pais.PaisId);
            }
            else
            {
                cboCiudades.DataSource = null;
            }
        }

    }
}
