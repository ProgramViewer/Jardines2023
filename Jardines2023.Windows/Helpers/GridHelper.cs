using Jardines2023.Entidades.Dtos;
using Jardines2023.Entidades.Dtos.Cliente;
using Jardines2023.Entidades.Dtos.Producto;
using Jardines2023.Entidades.Dtos.Proveedor;
using Jardines2023.Entidades.Entidades;
using System.Windows.Forms;

namespace Jardines2023.Windows.Helpers
{
    public static class GridHelper
    {
        public static void LimpiarGrilla(DataGridView dgv)
        {
            dgv.Rows.Clear();
        }
        public static DataGridViewRow ConstruirFila(DataGridView dgv)
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgv);
            return r;

        }
        public static void SetearFila(DataGridViewRow r, object obj)
        {
            switch (obj)
            {
                case Pais pais:
                    r.Cells[0].Value = pais.NombrePais;
                    break;
                case Ciudad ciudad:
                    r.Cells[0].Value = ciudad.Pais.NombrePais;
                    r.Cells[1].Value = ciudad.NombreCiudad;
                    break;
                case Categoria categoria:
                    r.Cells[0].Value = categoria.NombreCategoria;
                    r.Cells[1].Value = categoria.Descripción;
                    break;
                case ClienteListDto cliente:
                    r.Cells[0].Value = $"{cliente.Apellido}, {cliente.Nombre}";
                    r.Cells[1].Value = cliente.NombrePais;
                    r.Cells[2].Value = cliente.NombreCiudad;
                    break;
                case ProveedorListDto proveedor:
                    r.Cells[0].Value = proveedor.NombreProveedor;
                    r.Cells[1].Value = proveedor.NombrePais;
                    r.Cells[2].Value = proveedor.NombreCiudad;
                    break;
                case ProductoListDto producto:
                    r.Cells[0].Value = producto.NombreProducto;
                    r.Cells[1].Value = producto.Categoria;
                    r.Cells[2].Value = producto.PrecioUnitario;
                    r.Cells[3].Value = producto.UnidadesEnStock;
                    r.Cells[4].Value = producto.Suspendido;
                    break;
                case VentaListDto venta:
                    r.Cells[0].Value = venta.VentaId;
                    r.Cells[1].Value = venta.FechaVenta;
                    r.Cells[2].Value = venta.NombreCliente;
                    r.Cells[3].Value = venta.Total;
                    break;

            }
            r.Tag = obj;

        }
        public static void AgregarFila(DataGridView dgv, DataGridViewRow r)
        {
            dgv.Rows.Add(r);
        }

        public static void QuitarFila(DataGridView dgv, DataGridViewRow r)
        {
            dgv.Rows.Remove(r);
        }
    }
}
