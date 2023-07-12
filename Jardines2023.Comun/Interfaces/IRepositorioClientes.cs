using Jardines2023.Entidades.Dtos.Cliente;
using Jardines2023.Entidades.Entidades;
using System.Collections.Generic;

namespace Jardines2023.Comun.Interfaces
{
    public interface IRepositorioClientes
    {
        void Borrar(int clienteId);
        void Editar(Cliente cliente);
        bool Existe(Cliente cliente);
        List<ClienteListDto> Filtrar(Pais pais);
        int GetCantidad();
        List<ClienteListDto> GetClientes();
        List<ClienteListDto> GetClientesPorPagina(int registrosPorPagina, int paginaActual);
        void Agregar(Cliente cliente);
        Cliente GetClientePorId(int clienteId);
        List<ClienteListDto> GetClientes(Pais paisFiltro, Ciudad ciudadFiltro);
    }
}
