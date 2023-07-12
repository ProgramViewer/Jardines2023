using Jardines2023.Entidades.Dtos;
using Jardines2023.Entidades.Entidades;
using System.Collections.Generic;

namespace Jardines2023.Datos.Repositorios
{
    public interface IRepositorioVentas
    {
        void Agregar(Venta venta);
        void Borrar(int ventaId);
        void Editar(Venta venta);
        bool Existe(Venta venta);
        List<VentaListDto> Filtrar(Venta venta);
        int GetCantidad();
        Venta GetVentaPorId(int ventaId);
        List<VentaListDto> GetVentas();
        List<VentaListDto> GetVentas(Cliente clienteFiltro);
        List<VentaListDto> GetVentasPorPagina(int registrosPorPagina, int paginaActual);
    }
}