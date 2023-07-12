using Jardines2023.Entidades.Entidades;
using Jardines2023.Servicios.Interfaces;
using System;
using System.Windows.Forms;

namespace Jardines2023.Windows
{
    public partial class frmCategoriaAE : Form
    {
        private readonly IServiciosCategorias _servicio;
        private bool esEdicion=false;
        public frmCategoriaAE(IServiciosCategorias servicio)
        {
            InitializeComponent();
            _servicio = servicio;
        }

        public Categoria GetCategoria()
        {
            return categoria;
        }
        private Categoria categoria;

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (categoria==null)
                {
                    categoria = new Categoria();

                }
                categoria.NombreCategoria = txtCategoria.Text;
                categoria.Descripción = txtDescripcion.Text;
                try
                {

                    if (!_servicio.Existe(categoria))
                    {
                        _servicio.Guardar(categoria);

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
                            categoria = null;
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
                        categoria = null;
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
            txtCategoria.Clear();
            txtDescripcion.Clear();
            txtCategoria.Focus();
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(txtCategoria.Text))
            {
                valido = false;
                errorProvider1.SetError(txtCategoria, "Debe ingresar un nombre de país");

            }
            return valido;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (categoria!=null)
            {
                esEdicion = true;
                txtCategoria.Text = categoria.NombreCategoria;
                txtDescripcion.Text = categoria.Descripción;
            }
        }
        public void SetCategoria(Categoria categoria)
        {
            this.categoria = categoria;
        }
    }
}
