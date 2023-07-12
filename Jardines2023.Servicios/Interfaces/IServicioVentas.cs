using Jardines2023.Entidades.Dtos;
using Jardines2023.Entidades.Dtos.Cliente;
using Jardines2023.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jardines2023.Servicios.Interfaces
{
    public interface IServicioVentas
    {
        void Borrar(int ventaId);
        bool Existe(Venta venta);
        List<VentaListDto> Filtrar(Venta venta);
        int GetCantidad();
        Venta GetVentaPorId(int ventaId);
        List<VentaListDto> GetVentas();
        List<VentaListDto> GetVentas(Cliente clienteFiltro);
        List<VentaListDto> GetVentasPorPagina(int cantidad, int paginas);
        void Guardar(Venta venta);
    }
}
