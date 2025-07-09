using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DependienteDTO
    {
        public int IdDependiente { get; set; }

        public string Nombre { get; set; }
        public string Correo { get; set; }
        public long Telefono { get; set; }
        public string Dni { get; set; }
        public int IdEmpresa { get; set; }

        public DependienteDTO() { }

        public DependienteDTO(int idDependiente, string nombre,string correo, long telefono, string dni, int idEmpresa)
        {
            IdDependiente = idDependiente;
            Nombre = nombre;
            Correo = correo;
            Telefono = telefono;    
            Dni = dni;
            IdEmpresa = idEmpresa;
        }
    }
}
