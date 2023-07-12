using Jardines2023.Entidades.Entidades;
using Jardines2023.Servicios.Interfaces;
using Jardines2023.Windows.Helpers;
using System;
using System.Windows.Forms;

namespace Jardines2023.Windows
{
    public partial class frmClienteAE : Form
    {
        private readonly IServiciosClientes _servicios;
        private bool esEdicion = false;
        public frmClienteAE(IServiciosClientes servicios)
        {
            InitializeComponent();
            _servicios = servicios;
        }
        private Cliente cliente;
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CombosHelper.CargarComboPaises(ref cboPaises);
            if (cliente != null)
            {
                esEdicion = true;
                txtNombres.Text = cliente.Nombre;
                txtApellido.Text = cliente.Apellido;
                txtDireccion.Text= cliente.Direccion;
                txtCodPostal.Text = cliente.CodigoPostal;
                txtFijo.Text = cliente.TelefonoFijo;
                txtCelular.Text = cliente.TelefonoMovil;
                cboPaises.SelectedValue = cliente.PaisId;
                CombosHelper.CargarComboCiudades(ref cboCiudades, cliente.PaisId);
                cboCiudades.SelectedValue = cliente.CiudadId;
                txtEmail.Text = cliente.Email;
            }
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (cliente == null)
                {
                    cliente = new Cliente();
                }
                cliente.ClienteId= cliente.ClienteId;
                cliente.Nombre = txtNombres.Text;
                cliente.Apellido = txtApellido.Text;
                cliente.Direccion = txtDireccion.Text;
                cliente.CodigoPostal = txtCodPostal.Text;
                cliente.TelefonoFijo = txtFijo.Text;
                cliente.TelefonoMovil = txtCelular.Text;
                cliente.Pais = (Pais)cboPaises.SelectedItem;
                cliente.PaisId = (int)cboPaises.SelectedValue;
                cliente.Ciudad = (Ciudad)cboCiudades.SelectedItem;
                cliente.CiudadId = (int)cboCiudades.SelectedValue;
                cliente.Email = txtEmail.Text;

                try
                {

                    if (!_servicios.Existe(cliente))
                    {
                        _servicios.Guardar(cliente);

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
                                cliente = null;
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
                        cliente = null;
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
            txtNombres.Clear();
            txtApellido.Clear();
            txtCodPostal.Clear();
            txtDireccion.Clear();
            txtEmail.Clear();
            //cboCiudades.Items.Clear();
            cboPaises.SelectedIndex = 0;
            txtNombres.Focus();
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(txtNombres.Text))
            {
                valido = false;
                errorProvider1.SetError(txtNombres, "Los nombres son requeridos");
            }
            if (string.IsNullOrEmpty(txtApellido.Text))
            {
                valido = false;
                errorProvider1.SetError(txtApellido, "El apellido es requerido");
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

        public void SetCliente(Cliente cliente)
        {
            this.cliente = cliente;
        }

        public Cliente GetCliente()
        {
            return cliente;
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
