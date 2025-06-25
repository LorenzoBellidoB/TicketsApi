namespace ENT
{
    public class clsEmpresa
    {
        #region Propiedades

        public int IdEmpresa { get; set; }

        public string Nombre { get; set; }

        public string Cif {  get; set; }

        public string Calle { get; set; }

        public string Codigo_postal { get; set; }

        public string Localidad { get; set; }

        public string Provincia { get; set; }

        public string Direccion { get; set; }


        #endregion

        #region Constructores

        public clsEmpresa()
        {
        }

        public clsEmpresa(int idEmpresa)
        {
            IdEmpresa = idEmpresa;
        }

        public clsEmpresa(int idEmpresa, string nombre, string cif, string calle, string codigo_postal, string localidad, string provincia, string direccion)
        {
            IdEmpresa = idEmpresa;
            Nombre = nombre;
            Cif = cif;
            Calle = calle;
            Codigo_postal = codigo_postal;
            Localidad = localidad;
            Provincia = provincia;
            Direccion = direccion;
        }

        #endregion
    }
}
