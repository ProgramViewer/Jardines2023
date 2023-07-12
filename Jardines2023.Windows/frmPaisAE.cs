using Jardines2023.Entidades.Entidades;
using Jardines2023.Servicios.Interfaces;
using System;
using System.Windows.Forms;

namespace Jardines2023.Windows
{
    public partial class frmPaisAE : Form
    {
        private IServiciosPaises _servicio;
        public frmPaisAE(IServiciosPaises servicio)
        {
            InitializeComponent();
            _servicio = servicio;
        }
        private Pais pais;
        private bool esEdicion=false;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (pais!=null)
            {
                esEdicion = true;
                txtNombrePais.Text = pais.NombrePais;
            }
        }
        public Pais GetPais()
        {
            return pais;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (pais==null)
                {
                    pais=new Pais();

                }
                pais.NombrePais = txtNombrePais.Text;

                try
                {

                    if (!_servicio.Existe(pais))
                    {
                        _servicio.Guardar(pais);

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
                            pais = null;
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
                        pais = null;
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
            txtNombrePais.Clear();
            txtNombrePais.Focus();
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            if (string.IsNullOrEmpty(txtNombrePais.Text))
            {
                valido = false;
                errorProvider1.SetError(txtNombrePais, "Debe ingresar un nombre de país");

            }
            return valido;
        }

        public void SetPais(Pais pais)
        {
            this.pais = pais;
        }
    }
}
