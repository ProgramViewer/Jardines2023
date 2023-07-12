using Jardines2023.Entidades.Dtos.Proveedor;
using Jardines2023.Entidades.Entidades;
using System.Collections.Generic;

namespace Jardines2023.Comun.Interfaces
{
    public interface IRepositorioProveedores
    {
        void Borrar(int proveedorId);
        void Editar(Proveedor proveedor);
        bool Existe(Proveedor proveedor);
        List<ProveedorListDto> Filtrar(Pais pais);
        int GetCantidad();
        List<ProveedorListDto> GetProveedores();
        List<ProveedorListDto> GetProveedoresPorPagina(int registrosPorPagina, int paginaActual);
        void Agregar(Proveedor Proveedor);
        Proveedor GetProveedorPorId(int proveedorId);
    }
}
