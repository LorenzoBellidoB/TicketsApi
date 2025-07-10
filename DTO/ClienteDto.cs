using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ClienteDto
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Cif { get; set; }
        public string Calle { get; set; }
        public string Codigo_postal { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public int IdEmpresa { get; set; }

        public ClienteDto() { }

        public ClienteDto(int idCliente)
        {
            IdCliente = idCliente;
        }

        public ClienteDto(int idCliente, string nombre, string correo, string telefono, string calle, string codigo_postal, string localidad, string provincia)
        {
            IdCliente = idCliente;
            Nombre = nombre;
            Correo = correo;
            Telefono = telefono;
            Calle = calle;
            Codigo_postal = codigo_postal;
            Localidad = localidad;
            Provincia = provincia;
        }

    }
}
