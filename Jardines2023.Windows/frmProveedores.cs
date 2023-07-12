using Jardines2023.Entidades.Dtos.Proveedor;
using Jardines2023.Entidades.Entidades;
using Jardines2023.Servicios.Interfaces;
using Jardines2023.Servicios.Servicios;
using Jardines2023.Windows.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Jardines2023.Windows
{
    public partial class frmProveedores : Form
    {
        //Para paginación
        int paginaActual = 1;
        int registros = 0;
        int paginas = 0;
        int registrosPorPagina = 12;

        public frmProveedores()
        {
            InitializeComponent();
            _servicio = new ServiciosProveedores();
        }
        private readonly IServiciosProveedores _servicio;
        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        List<ProveedorListDto> lista;
        private void frmProveedores_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
        }
        private void RecargarGrilla()
        {
            try
            {
                registros = _servicio.GetCantidad();
                paginas = FormHelper.CalcularPaginas(registros, registrosPorPagina);
                MostrarPaginado();
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void MostrarDatosEnGrilla()
        {
            GridHelper.LimpiarGrilla(dgvDatos);
            foreach (var proveedor in lista)
            {
                var r = GridHelper.ConstruirFila(dgvDatos);
                GridHelper.SetearFila(r, proveedor);
                GridHelper.AgregarFila(dgvDatos, r);
            }
            lblRegistros.Text = registros.ToString();
            lblPaginaActual.Text = paginaActual.ToString();
            lblPaginas.Text = paginas.ToString();


        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmProveedorAE frm = new frmProveedorAE(_servicio) { Text = "Agregar Proveedor" };
            DialogResult dr = frm.ShowDialog(this);
            RecargarGrilla();
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            ProveedorListDto proveedorDto = (ProveedorListDto)r.Tag;
            Proveedor proveedor = _servicio.GetProveedorPorId(proveedorDto.ProveedorId);
            Proveedor proveedorCopia = (Proveedor)proveedor.Clone();

            try
            {
                frmProveedorAE frm = new frmProveedorAE(_servicio) { Text = "Editar Proveedor" };
                frm.SetProveedor(proveedor);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    GridHelper.SetearFila(r, proveedorCopia);

                    return;
                }
                proveedor = frm.GetProveedor();
                if (proveedor != null)
                {
                    GridHelper.SetearFila(r, proveedor);

                }
                else
                {
                    GridHelper.SetearFila(r, proveedorCopia);

                }
            }
            catch (Exception ex)
            {
                GridHelper.SetearFila(r, proveedorCopia);
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            ProveedorListDto proveedor = (ProveedorListDto)r.Tag;
            try
            {
                //TODO: Se debe controlar que no este relacionado
                DialogResult dr = MessageBox.Show("¿Desea borrar el registro seleccionado?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No) { return; }
                _servicio.Borrar(proveedor.ProveedorId);
                GridHelper.QuitarFila(dgvDatos, r);
                registros = _servicio.GetCantidad();
                paginas = FormHelper.CalcularPaginas(registros, registrosPorPagina);
                lblRegistros.Text = registros.ToString();
                lblPaginas.Text = paginas.ToString();
                //lblCantidad.Text = _servicio.GetCantidad().ToString();
                MessageBox.Show("Registro borrado", "Mensaje",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            frmSeleccionarPais frm = new frmSeleccionarPais() { Text = "Seleccionar País" };
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                var pais = frm.GetPais();
                lista = _servicio.Filtrar(pais);
                tsbBuscar.BackColor = Color.Orange;

                MostrarDatosEnGrilla();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            RecargarGrilla();
            tsbBuscar.BackColor = Color.White;
        }



        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual == paginas)
            {
                return;
            }
            paginaActual++;
            MostrarPaginado();

        }
        private void MostrarPaginado()
        {
            lista = _servicio.GetProveedoresPorPagina(registrosPorPagina, paginaActual);
            MostrarDatosEnGrilla();
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

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            paginaActual = 1;
            MostrarPaginado();
        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private Pais paisFiltro;
        private Ciudad ciudadFiltro;
        private void porPaisYCiudadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBuscarPaisCiudad frm = new frmBuscarPaisCiudad() { Text = "Seleccionar País y Ciudad a Filtrar" };
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                paisFiltro = frm.GetPais();
                ciudadFiltro = frm.GetCiudad();
                lista = _servicio.GetProveedores(paisFiltro, ciudadFiltro);
                MostrarDatosEnGrilla();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void porNombreToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


    }
}
