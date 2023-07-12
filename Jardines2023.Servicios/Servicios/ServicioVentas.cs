using Jardines2023.Datos.Repositorios;
using Jardines2023.Entidades.Dtos;
using Jardines2023.Entidades.Entidades;
using Jardines2023.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jardines2023.Servicios.Servicios
{
    public class ServicioVentas : IServicioVentas
    {
        private readonly IRepositorioVentas _repositorio;
        public ServicioVentas()
        {
            _repositorio=new RepositorioVentas();
        }
        public void Borrar(int ventaId)
        {
            try
            {
                _repositorio.Borrar(ventaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Venta venta)
        {
            try
            {
                return _repositorio.Existe(venta);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<VentaListDto> Filtrar(Venta venta)
        {
            try
            {
                return _repositorio.Filtrar(venta);
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

        public Venta GetVentaPorId(int ventaId)
        {
            try
            {
                return _repositorio.GetVentaPorId(ventaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<VentaListDto> GetVentas()
        {
            try
            {
                return _repositorio.GetVentas();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<VentaListDto> GetVentas(Cliente clienteFiltro)
        {
            try
            {
                return _repositorio.GetVentas(clienteFiltro);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<VentaListDto> GetVentasPorPagina(int cantidad, int paginas)
        {
            try
            {
                return _repositorio.GetVentasPorPagina(cantidad, paginas);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Venta venta)
        {
            try
            {
                if (venta.VentaId == 0)
                {
                    _repositorio.Agregar(venta);
                }
                else
                {
                    _repositorio.Editar(venta);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
