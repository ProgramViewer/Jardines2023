using Jardines2023.Entidades.Entidades;
using Jardines2023.Servicios.Interfaces;
using Jardines2023.Servicios.Servicios;
using Jardines2023.Windows.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jardines2023.Windows
{
    public partial class frmCiudadAE : Form
    {
        private IServiciosCiudades _servicio;
        public frmCiudadAE(IServiciosCiudades servicio)
        {
            _servicio = servicio;
            InitializeComponent();
        }
        private Ciudad ciudad;
        private bool esEdicion=false;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CombosHelper.CargarComboPaises(ref cboPaises);
            if (ciudad!=null)
            {
                txtNombreCiudad.Text = ciudad.NombreCiudad;
                cboPaises.SelectedValue = ciudad.PaisId;
                esEdicion = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (ciudad==null)
                {
                    ciudad=new Ciudad();
                }
                ciudad.NombreCiudad = txtNombreCiudad.Text;
                ciudad.Pais = (Pais)cboPaises.SelectedItem;
                ciudad.PaisId = (int)cboPaises.SelectedValue;

                try
                {
                    
                    if (!_servicio.Existe(ciudad))
                    {
                        _servicio.Guardar(ciudad);

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
                            ciudad = null;
                            InicializarControles();

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
                        ciudad = null;
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
            cboPaises.SelectedIndex = 0;
            txtNombreCiudad.Clear();
            cboPaises.Focus();
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            if (cboPaises.SelectedIndex==0)
            {
                valido = false;
                errorProvider1.SetError(cboPaises, "Debe seleccionar un país");
            }
            if (string.IsNullOrEmpty(txtNombreCiudad.Text))
            {
                valido = false;
                errorProvider1.SetError(txtNombreCiudad, "El nombre es requerido");
            }
            return valido;
        }

        public Ciudad GetCiudad()
        {
            return ciudad;
        }

        public void SetCiudad(Ciudad ciudad)
        {
            this.ciudad=ciudad;
        }

        private void btnAgregarPais_Click(object sender, EventArgs e)
        {
            var _servicioPaises = new ServiciosPaises();
            frmPaisAE frm = new frmPaisAE(_servicioPaises) { Text = "Agregar país" };
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel) return;
            try
            {
                var pais = frm.GetPais();
                if (!_servicioPaises.Existe(pais))
                {
                    _servicioPaises.Guardar(pais);
                    MessageBox.Show("Registro agregado",
                        "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Registro existente",
                        "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,
                    "Mensaje",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            CombosHelper.CargarComboPaises(ref cboPaises);
        }
    }
}
