using Jardines2023.Entidades.Entidades;
using Jardines2023.Servicios.Interfaces;
using Jardines2023.Windows.Helpers;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Jardines2023.Windows
{
    public partial class frmProductoAE : Form
    {
        private string imagenNoDisponible = Environment.CurrentDirectory + @"\Imagenes\SinImagenDisponible.jpg";
        private string archivoNoEncontrado = Environment.CurrentDirectory + @"\Imagenes\ArchivoNoEncontrado.jpg";
        private string archivoImagen = string.Empty;
        private string carpetaImagen = Environment.CurrentDirectory + @"\Imagenes\";

        private readonly IServiciosProductos _servicio;
        public frmProductoAE(IServiciosProductos servicio)
        {
            InitializeComponent();
            _servicio = servicio;
        }
        private Producto producto;
        private bool esEdicion = false;
        public Producto GetProducto()
        {
            return producto;
        }
       
        public void SetProducto(Producto producto)
        {
            this.producto = producto;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (producto == null)
                {
                    producto = new Producto();

                }
                producto.NombreProducto = txtProducto.Text;
                producto.NombreLatin = txtLatin.Text;
                producto.Suspendido=chkSuspendido.Checked;
                producto.CategoriaId = (int)cboCategorias.SelectedValue;
                producto.ProveedorId = (int)cboProveedores.SelectedValue;
                producto.PrecioUnitario=decimal.Parse(txtPrecioVta.Text);
                producto.UnidadesEnStock = (int)nudStock.Value;
                producto.NivelDeReposicion = (int)nudMinimo.Value;
                producto.Imagen = archivoImagen;
                try
                {

                    if (!_servicio.Existe(producto))
                    {
                        _servicio.Guardar(producto);

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
                            producto = null;
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
                        producto = null;
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
            txtProducto.Clear();
            txtLatin.Clear();
            txtPrecioVta.Clear();
            nudMinimo.Value = 1;
            nudMinimo.Value = 1;
            cboCategorias.SelectedIndex = 0;
            cboProveedores.SelectedIndex = 0;
            chkSuspendido.Checked = false;
            txtProducto.Focus();
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            return valido;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CombosHelper.CargarComboCategorias(ref cboCategorias);
            CombosHelper.CargarComboProveedores(ref cboProveedores);

            if (producto != null)
            {
                esEdicion = true;
                txtProducto.Text = producto.NombreProducto;
                txtLatin.Text = producto.NombreLatin;
                txtPrecioVta.Text = producto.PrecioUnitario.ToString();
                nudMinimo.Value = producto.NivelDeReposicion;
                nudStock.Value = producto.UnidadesEnStock;
                cboCategorias.SelectedValue = producto.CategoriaId;
                cboProveedores.SelectedValue = producto.ProveedorId;

                //Veo si el producto tiene alguna imagen asociada
                if (producto.Imagen != string.Empty)
                {
                    //Me aseguro que esa imagen exista
                    if (!File.Exists($"{carpetaImagen}{producto.Imagen}"))
                    {
                        //Si no existe, muestro la imagen de archivo no encontrado
                        picImagen.Image = Image.FromFile(archivoNoEncontrado);
                    }
                    else
                    {
                        //Caso contrario muestro la imagen
                        picImagen.Image = Image.FromFile($"{carpetaImagen}{producto.Imagen}");
                    }
                }
                else
                {
                    //Si no tiene imagen muestro Sin Imagen 
                    picImagen.Image = Image.FromFile(imagenNoDisponible);
                }

            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Seteo del openFileDialog
            openFileDialog1.Multiselect = false;
            openFileDialog1.InitialDirectory = Environment.CurrentDirectory + @"\Imagenes\";
            openFileDialog1.Filter = "Archivos jpg (*.jpg)|*.jpg|Archivos png (*.png)|*.png|Archivos jfif (*.jfif)|*.jfif";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            DialogResult dr = openFileDialog1.ShowDialog(this);//muestro el openFileDialog

            if (dr == DialogResult.OK)
            {
                //Veo si tengo algun imagen seleccionada
                if (openFileDialog1.FileName == null)
                {
                    return;//sino me voy
                }
                //Tomo el nombre del archivo de imagen con su ruta
                //archivoNombreConRuta = openFileDialog1.FileName;
                picImagen.Image = Image.FromFile(openFileDialog1.FileName);
                archivoImagen = openFileDialog1.FileName;//Tomo la ruta y el nombre del archivo
            }

        }
    }
}
