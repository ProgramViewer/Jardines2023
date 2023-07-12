namespace Jardines2023.Entidades.Entidades
{
    public class Proveedor
    {
        public int ProveedorId { get; set; }
        public string NombreProveedor { get; set; }
        public string Direccion { get; set; }
        public string CodigoPostal { get; set; }
        public string TelefonoFijo { get; set; }
        public string TelefonoMovil { get; set; }
        public int PaisId { get; set; }
        public int CiudadId { get; set; }
        public Pais Pais { get; set; }
        public Ciudad Ciudad { get; set; }
        public string Email { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
