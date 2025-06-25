using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENT
{
    public class clsTicket
    {
        #region Propiedades
        public int IdTicket { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public int IdDependiente { get; set; }
        public int IdEmpresa { get; set; }

        public int IdAlbaran { get; set; } 
        #endregion
        #region Constructores
        public clsTicket()
        {
        }
        public clsTicket(int idTicket)
        {
            IdTicket = idTicket;
        }
        public clsTicket(int idTicket, DateTime fecha, int idCliente, int idDependiente, int idEmpresa)
        {
            IdTicket = idTicket;
            Fecha = fecha;
            IdCliente = idCliente;
            IdDependiente = idDependiente;
            IdEmpresa = idEmpresa;
        }
        #endregion
    }
}
