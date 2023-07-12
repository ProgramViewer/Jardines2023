using Jardines2023.Entidades.Entidades;
using System.Collections.Generic;

namespace Jardines2023.Comun.Interfaces
{
    public interface IRepositorioCategorias
    {
        void Agregar(Categoria categoria);
        void Borrar(int categoriaId);
        void Editar(Categoria categoria);
        bool Existe(Categoria categoria);
        int GetCantidad();
        List<Categoria> GetCategorias();
        List<Categoria> GetCategoriasPorPagina(int cantidad, int pagina);
    }
}