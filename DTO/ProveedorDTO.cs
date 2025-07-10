using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProveedorDTO
    {
        public int IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string Cif { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Calle { get; set; }
        public string Codigo_postal { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public int IdEmpresa { get; set; }

        public ProveedorDTO() { }

        public ProveedorDTO(int idProveedor)
        {
            IdProveedor = idProveedor;
        }

        public ProveedorDTO(int idProveedor, string nombre, string cif, string direccion, string telefono, string correo, string calle, string codigo_postal, string localidad, string provincia, int idEmpresa)
        {
            IdProveedor = idProveedor;
            Nombre = nombre;
            Cif = cif;
            Telefono = telefono;
            Correo = correo;
            Calle = calle;
            Codigo_postal = codigo_postal;
            Localidad = localidad;
            Provincia = provincia;
            IdEmpresa = idEmpresa;
        }
    }
}
