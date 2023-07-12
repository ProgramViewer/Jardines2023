using Jardines2023.Comun.Interfaces;
using Jardines2023.Comun.Repositorios;
using Jardines2023.Entidades.Dtos.Proveedor;
using Jardines2023.Entidades.Entidades;
using Jardines2023.Servicios.Interfaces;
using System;
using System.Collections.Generic;

namespace Jardines2023.Servicios.Servicios
{
    public class ServiciosProveedores : IServiciosProveedores
    {
        private readonly IRepositorioProveedores _repositorio;
        public ServiciosProveedores()
        {
            _repositorio = new RepositorioProveedores();
        }

        public void Borrar(int proveedorId)
        {
            try
            {
                _repositorio.Borrar(proveedorId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Proveedor proveedor)
        {
            try
            {
                return _repositorio.Existe(proveedor);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProveedorListDto> Filtrar(Pais pais)
        {
            throw new NotImplementedException();
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


        public List<ProveedorListDto> GetProveedores(Pais paisFiltro, Ciudad ciudadFiltro)
        {
            throw new NotImplementedException();
        }


        public Proveedor GetProveedorPorId(int proveedorId)
        {
            try
            {
                return _repositorio.GetProveedorPorId(proveedorId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProveedorListDto> GetProveedores()
        {
            try
            {
                return _repositorio.GetProveedores();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public List<ProveedorListDto> GetProveedores(Pais paisFiltro, Ciudad ciudadFiltro)
        //{
        //    try
        //    {
        //        return _repositorio.GetProveedores(paisFiltro, ciudadFiltro);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public List<ProveedorListDto> GetProveedoresPorPagina(int cantidad, int pagina)
        {
            try
            {
                return _repositorio.GetProveedoresPorPagina(cantidad, pagina);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Proveedor proveedor)
        {
            try
            {
                if (proveedor.ProveedorId == 0)
                {
                    _repositorio.Agregar(proveedor);
                }
                else
                {
                    _repositorio.Editar(proveedor);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
