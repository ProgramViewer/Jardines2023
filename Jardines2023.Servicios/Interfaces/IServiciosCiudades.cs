using Jardines2023.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jardines2023.Servicios.Interfaces
{
    public interface IServiciosCiudades
    {
        void Guardar(Ciudad ciudad);
        void Borrar(int ciudadId);
        bool Existe(Ciudad ciudad);
        //List<Ciudad> GetCiudades();
        List<Ciudad> Filtrar(Pais pais);
        List<Ciudad> GetCiudadesPorPagina(int registrosPorPagina, int paginaActual);
        List<Ciudad> GetCiudades(int? paisId);
        int GetCantidad(int? paisId);
    }
}
