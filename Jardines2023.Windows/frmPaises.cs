using Jardines2023.Entidades.Entidades;
using Jardines2023.Servicios.Servicios;
using Jardines2023.Windows.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Jardines2023.Windows
{
    public partial class frmPaises : Form
    {
        public frmPaises()
        {
            InitializeComponent();
            _servicio = new ServiciosPaises();
        }
        private readonly ServiciosPaises _servicio;
        private List<Pais> lista;

        //Para paginación
        int paginaActual = 1;
        int registros = 0;
        int paginas = 0;
        int registrosPorPagina = 12;

        bool filtroOn=false;
        string textoFiltro = null;
        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmPaises_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
        }


        private void MostrarDatosEnGrilla()
        {
            GridHelper.LimpiarGrilla(dgvDatos);
            foreach (var pais in lista)
            {
                DataGridViewRow r = GridHelper.ConstruirFila(dgvDatos);
                GridHelper.SetearFila(r, pais);
                GridHelper.AgregarFila(dgvDatos, r);
            }
            lblRegistros.Text = registros.ToString();
            lblPaginaActual.Text = paginaActual.ToString();
            lblPaginas.Text=paginas.ToString();
        }


        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmPaisAE frm = new frmPaisAE(_servicio) { Text = "Agregar país" };
            DialogResult dr = frm.ShowDialog(this);
            RecargarGrilla();
        }

        private void RecargarGrilla()
        {
            try
            {
                registros = _servicio.GetCantidad(null);
                paginas = FormHelper.CalcularPaginas(registros, registrosPorPagina);
                MostrarPaginado();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count==0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            Pais pais = (Pais)r.Tag;
            try
            {
                //TODO: Se debe controlar que no este relacionado
                DialogResult dr=MessageBox.Show("¿Desea borrar el registro seleccionado?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No) { return; }
                _servicio.Borrar(pais.PaisId);
                GridHelper.QuitarFila(dgvDatos,r);
                //lblCantidad.Text = _servicio.GetCantidad().ToString();
                MessageBox.Show("Registro borrado","Mensaje",
                    MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            Pais pais = (Pais)r.Tag;
            Pais paisCopia =(Pais) pais.Clone();
            try
            {
                frmPaisAE frm = new frmPaisAE(_servicio) { Text = "Editar País" };
                frm.SetPais(pais);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    GridHelper.SetearFila(r, paisCopia);

                    return;
                }
                pais = frm.GetPais();
                if (pais != null)
                {
                    GridHelper.SetearFila(r, pais);

                }
                else
                {
                    GridHelper.SetearFila(r, paisCopia);

                }
            }
            catch (Exception ex)
            {
                GridHelper.SetearFila(r, paisCopia);
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual==paginas)
            {
                return;
            }
            paginaActual++;
            MostrarPaginado();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual == 1)
            {
                return;
            }
            paginaActual--;
            MostrarPaginado();
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {

            paginaActual = paginas;
            MostrarPaginado();

        }

        private void MostrarPaginado()
        {
            lista = _servicio.GetPaisesPorPagina(registrosPorPagina, paginaActual,textoFiltro);
            MostrarDatosEnGrilla();
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            paginaActual = 1;
            MostrarPaginado();
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            if (!filtroOn)
            {
                frmBuscarPorNombre frm = new frmBuscarPorNombre() { Text = "Buscar por Nombre de País" };
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                try
                {
                    textoFiltro = frm.GetTexto();
                    tsbBuscar.BackColor = Color.Orange;
                    filtroOn = true;
                    lista = _servicio.GetPaises(textoFiltro);
                    registros = _servicio.GetCantidad(textoFiltro);
                    paginas = FormHelper.CalcularPaginas(registros, registrosPorPagina);
                    MostrarDatosEnGrilla();
                }
                catch (Exception)
                {

                    throw;
                }

            }
            else
            {
                MessageBox.Show("Quite el filtro activo!!!", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            filtroOn=false;
            tsbBuscar.BackColor = Color.White;
            textoFiltro=null;
            RecargarGrilla();

        }
    }
}
