using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DependienteDTO
    {
        public int IdDependiente { get; set; }

        public string Nombre { get; set; }

        public int IdEmpresa { get; set; }

        public DependienteDTO() { }

        public DependienteDTO(int idDependiente, string nombre, int idEmpresa)
        {
            IdDependiente = idDependiente;
            Nombre = nombre;
            IdEmpresa = idEmpresa;
        }
    }
}
