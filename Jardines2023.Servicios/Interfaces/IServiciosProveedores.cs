using Jardines2023.Entidades.Dtos.Cliente;
using Jardines2023.Entidades.Dtos.Proveedor;
using Jardines2023.Entidades.Entidades;
using System.Collections.Generic;

namespace Jardines2023.Servicios.Interfaces
{
    public interface IServiciosProveedores
    {
        void Borrar(int proveedorId);
        bool Existe(Proveedor proveedor);
        List<ProveedorListDto> Filtrar(Pais pais);
        int GetCantidad();
        Proveedor GetProveedorPorId(int proveedorId);
        List<ProveedorListDto> GetProveedores();
        List<ProveedorListDto> GetProveedores(Pais paisFiltro, Ciudad ciudadFiltro);
        List<ProveedorListDto> GetProveedoresPorPagina(int registrosPorPagina, int paginaActual);
        void Guardar(Proveedor proveedor);

    }
}
