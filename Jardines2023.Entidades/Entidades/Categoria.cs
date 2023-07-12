using System;

namespace Jardines2023.Entidades.Entidades
{
    public class Categoria:ICloneable
    {
        public int CategoriaId { get; set; }
        public string NombreCategoria { get; set; }
        public string Descripción { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
