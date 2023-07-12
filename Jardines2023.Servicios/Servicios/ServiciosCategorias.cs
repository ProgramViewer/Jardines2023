using Jardines2023.Comun.Repositorios;
using Jardines2023.Entidades.Entidades;
using Jardines2023.Servicios.Interfaces;
using System;
using System.Collections.Generic;

namespace Jardines2023.Servicios.Servicios
{
    public class ServiciosCategorias : IServiciosCategorias
    {
        private readonly RepositorioCategorias _repositorio;
        public ServiciosCategorias()
        {
            _repositorio = new RepositorioCategorias();
        }
        public void Borrar(int categoriaId)
        {
            try
            {
                _repositorio.Borrar(categoriaId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Categoria> GetCategorias()
        {
            try
            {
                return _repositorio.GetCategorias();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int GetCantidad()
        {
            try
            {
                return _repositorio.GetCantidad();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Categoria categoria)
        {
            try
            {
                return _repositorio.Existe(categoria);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Categoria categoria)
        {
            try
            {
                if (categoria.CategoriaId==0)
                {
                    _repositorio.Agregar(categoria);

                }
                else
                {
                    _repositorio.Editar(categoria);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<Categoria> GetCategoriasPorPagina(int cantidad, int pagina)
        {
             try
            {
                return _repositorio.GetCategoriasPorPagina(cantidad, pagina);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
