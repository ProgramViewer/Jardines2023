using Jardines2023.Comun.Interfaces;
using Jardines2023.Comun.Repositorios;
using Jardines2023.Entidades.Dtos.Cliente;
using Jardines2023.Entidades.Entidades;
using Jardines2023.Servicios.Interfaces;
using System;
using System.Collections.Generic;

namespace Jardines2023.Servicios.Servicios
{
    public class ServiciosClientes : IServiciosClientes
    {
        private readonly IRepositorioClientes _repositorio;
        public ServiciosClientes()
        {
            _repositorio = new RepositorioClientes();
        }

        public void Borrar(int clienteId)
        {
            try
            {
                _repositorio.Borrar(clienteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Cliente cliente)
        {
            try
            {
                return _repositorio.Existe(cliente);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ClienteListDto> Filtrar(Pais pais)
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

        public Cliente GetClientePorId(int clienteId)
        {
            try
            {
                return _repositorio.GetClientePorId(clienteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ClienteListDto> GetClientes()
        {
            try
            {
                return _repositorio.GetClientes();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ClienteListDto> GetClientes(Pais paisFiltro, Ciudad ciudadFiltro)
        {
            try
            {
                return _repositorio.GetClientes(paisFiltro, ciudadFiltro);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ClienteListDto> GetClientesPorPagina(int cantidad, int pagina)
        {
            try
            {
                return _repositorio.GetClientesPorPagina(cantidad, pagina);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Cliente cliente)
        {
            try
            {
                if (cliente.ClienteId == 0)
                {
                    _repositorio.Agregar(cliente);
                }
                else
                {
                    _repositorio.Editar(cliente);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
