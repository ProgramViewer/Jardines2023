using Jardines2023.Entidades.Entidades;
using System.Collections.Generic;

namespace Jardines2023.Comun.Interfaces
{
    public interface IRepositorioPaises
    {
        void Agregar(Pais pais);
        void Borrar(int paisId);
        void Editar(Pais pais);
        bool Existe(Pais pais);
        //int GetCantidad();
        int GetCantidad(string textoFiltro);
        List<Pais> GetPaises();
        List<Pais> GetPaises(string textoFiltro);
        List<Pais> GetPaisesPorPagina(int cantidad, int paginaActual, string textoFiltro);
        Pais GetPaisPorId(int paisId);
    }
}