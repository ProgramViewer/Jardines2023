using Jardines2023.Comun.Interfaces;
using Jardines2023.Comun.Repositorios;
using Jardines2023.Entidades.Entidades;
using Jardines2023.Servicios.Interfaces;
using System;
using System.Collections.Generic;

namespace Jardines2023.Servicios.Servicios
{
    public class ServiciosPaises : IServiciosPaises
    {
        //private readonly RepositorioPaises _repositorio;
        private readonly IRepositorioPaises _repositorio;
        public ServiciosPaises()
        {
            _repositorio = new RepositorioPaises();
        }
        public void Borrar(int paisId)
        {
            try
            {
                _repositorio.Borrar(paisId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Pais> GetPaises()
        {
            try
            {
                return _repositorio.GetPaises();
            }
            catch (Exception)
            {

                throw;
            }
        }
        //public int GetCantidad()
        //{
        //    try
        //    {
        //        return _repositorio.GetCantidad();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public bool Existe(Pais pais)
        {
            try
            {
                return _repositorio.Existe(pais);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Pais pais)
        {
            try
            {
                if (pais.PaisId == 0)
                {
                    _repositorio.Agregar(pais);

                }
                else
                {
                    _repositorio.Editar(pais);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Pais GetPaisPorId(int paisId)
        {
            try
            {
                return _repositorio.GetPaisPorId(paisId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Pais> GetPaisesPorPagina(int cantidad, int paginaActual, string textoFiltro = null)
        {
            try
            {
                return _repositorio.GetPaisesPorPagina(cantidad, paginaActual, textoFiltro);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Pais> GetPaises(string textoFiltro)
        {
            try
            {
                return _repositorio.GetPaises(textoFiltro);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad(string textoFiltro = null)
        {
            try
            {
                return _repositorio.GetCantidad(textoFiltro);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
