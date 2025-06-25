using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENT
{
    public class clsCliente
    {
        #region Propiedades
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }

        public string Cif { get; set; }
        public string Calle { get; set; }

        public string Codigo_postal { get; set; }

        public string Localidad { get; set; }

        public string Provincia { get; set; }

        public string Direccion { get; set; }
        #endregion

        #region Constructores
        public clsCliente()
        {
        }
        public clsCliente(int idCliente)
        {
            IdCliente = idCliente;
        }

        public clsCliente(int idCliente, string nombre, string correo, string telefono, string calle, string codigo_postal, string localidad, string provincia, string direccion)
        {
            IdCliente = idCliente;
            Nombre = nombre;
            Correo = correo;
            Telefono = telefono;
            Calle = calle;
            Codigo_postal = codigo_postal;
            Localidad = localidad;
            Provincia = provincia;
            Direccion = direccion;
        }

        #endregion
    }
}
