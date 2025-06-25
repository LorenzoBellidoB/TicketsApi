using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENT
{
    public class clsDetalleTicket
    {
        #region Propiedades
        public int IdDetalleTicket { get; set; }
        public int IdTicket { get; set; }
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        #endregion
        #region Constructores
        public clsDetalleTicket()
        {
        }
        public clsDetalleTicket(int idDetalleTicket)
        {
            IdDetalleTicket = idDetalleTicket;
        }
        public clsDetalleTicket(int idDetalleTicket, int idTicket, int idProducto, decimal precio_kilo, decimal cantidad, decimal impuesto)
        {
            IdDetalleTicket = idDetalleTicket;
            IdTicket = idTicket;
            IdProducto = idProducto;
            Cantidad = cantidad;
        }
        #endregion
    }
}
