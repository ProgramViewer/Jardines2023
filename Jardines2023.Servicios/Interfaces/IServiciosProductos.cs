using Jardines2023.Entidades.Dtos.Producto;
using Jardines2023.Entidades.Entidades;
using System.Collections.Generic;

namespace Jardines2023.Servicios.Interfaces
{
    public interface IServiciosProductos
    {
        void Guardar(Producto producto);
        void Borrar(int productoId);
        bool Existe(Producto producto);
        int GetCantidad();
        List<ProductoListDto> GetProductos();
        List<ProductoListDto> GetProductosPorPagina(int cantidad, int pagina);
        Producto GetProductoPorId(int productoId);
    }
}
