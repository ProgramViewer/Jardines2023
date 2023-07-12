using Jardines2023.Entidades.Dtos.Proveedor;
using Jardines2023.Entidades.Entidades;
using Jardines2023.Servicios.Interfaces;
using Jardines2023.Servicios.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jardines2023.Windows.Helpers
{
    public static class CombosHelper
    {
        public static void CargarComboPaises(ref ComboBox combo)
        {
            IServiciosPaises serviciosPaises = new ServiciosPaises();
            var lista = serviciosPaises.GetPaises();
            var defaultPais = new Pais()
            {
                PaisId = 0,
                NombrePais = "Seleccione País"
            };
            lista.Insert(0,defaultPais);
            combo.DataSource=lista;
            combo.DisplayMember = "NombrePais";
            combo.ValueMember = "PaisId";
            combo.SelectedIndex=0;
        }
        public static void CargarComboCiudades(ref ComboBox combo, int paisId)
        {
            IServiciosCiudades serviciosCiudades=new ServiciosCiudades();
            var lista = serviciosCiudades.GetCiudades(paisId);
            var defaultCiudad = new Ciudad()
            {
                CiudadId = 0,
                NombreCiudad = "Seleccione Ciudad"
            };
            lista.Insert(0,defaultCiudad);
            combo.DataSource = lista;
            combo.DisplayMember = "NombreCiudad";
            combo.ValueMember = "CiudadId";
            combo.SelectedIndex = 0;

        }

        public static void CargarComboCategorias(ref ComboBox combo)
        {
            IServiciosCategorias serviciosCategorias = new ServiciosCategorias();
            var lista = serviciosCategorias.GetCategorias();
            var defaultCategoria = new Categoria()
            {
                CategoriaId = 0,
                NombreCategoria = "Seleccione Categoría"
            };
            lista.Insert(0, defaultCategoria);
            combo.DataSource = lista;
            combo.DisplayMember = "NombreCategoria";
            combo.ValueMember = "CategoriaId";
            combo.SelectedIndex = 0;
        }

        public static void CargarComboProveedores(ref ComboBox combo)
        {
            IServiciosProveedores serviciosProveedores = new ServiciosProveedores();
            var lista = serviciosProveedores.GetProveedores();
            var defaultProveedor = new ProveedorListDto()
            {
                ProveedorId = 0,
                NombreProveedor = "Seleccione Proveedor"
            };
            lista.Insert(0, defaultProveedor);
            combo.DataSource = lista;
            combo.DisplayMember = "NombreProveedor";
            combo.ValueMember = "ProveedorId";
            combo.SelectedIndex = 0;
        }
    }
}
