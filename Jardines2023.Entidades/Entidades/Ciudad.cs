using System;

namespace Jardines2023.Entidades.Entidades
{
    public class Ciudad:ICloneable
    {
        public int CiudadId { get; set; }
        public string NombreCiudad { get; set; }
        public int PaisId { get; set; }
        public Pais Pais { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
