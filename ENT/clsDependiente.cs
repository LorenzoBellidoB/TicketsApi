using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENT
{
    public class clsDependiente
    {
        #region Propiedades
        public int IdDependiente { get; set; }

        public string Nombre { get; set; }

        public int IdEmpresa { get; set; }

        #endregion

        #region Constructores
        public clsDependiente()
        {
        }
        public clsDependiente(int idDependiente)
        {
            IdDependiente = idDependiente;
        }
        public clsDependiente(int idDependiente, string nombre, int idEmpresa)
        {
            IdDependiente = idDependiente;
            Nombre = nombre;
            IdEmpresa = idEmpresa;
        }
        #endregion
    }
}
