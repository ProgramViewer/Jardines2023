using Jardines2023.Entidades.Dtos.Cliente;
using Jardines2023.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jardines2023.Servicios.Interfaces
{
    public interface IServiciosClientes
    {
        void Borrar(int clienteId);
        bool Existe(Cliente cliente);
        List<ClienteListDto> Filtrar(Pais pais);
        int GetCantidad();
        Cliente GetClientePorId(int clienteId);
        List<ClienteListDto> GetClientes();
        List<ClienteListDto> GetClientes(Pais paisFiltro, Ciudad ciudadFiltro);
        List<ClienteListDto> GetClientesPorPagina(int registrosPorPagina, int paginaActual);
        void Guardar(Cliente cliente);
    }
}
