using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENT
{
    public class clsProveedore
    {
        #region Propiedades
        public int IdProveedor { get; set; }

        public string Nombre { get; set; }
        public string Cif { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Calle { get; set; }
        public string Codigo_postal { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }

        #endregion

        #region Constructores
        public clsProveedore()
        {
        }
        public clsProveedore(int idProveedor)
        {
            IdProveedor = idProveedor;
        }
        public clsProveedore(int idProveedor, string nombre, string cif, string direccion, string telefono, string correo, string calle, string codigo_postal, string localidad, string provincia)
        {
            IdProveedor = idProveedor;
            Nombre = nombre;
            Cif = cif;
            Direccion = direccion;
            Telefono = telefono;
            Correo = correo;
            Calle = calle;
            Codigo_postal = codigo_postal;
            Localidad = localidad;
            Provincia = provincia;
        }
        #endregion
    }
}
