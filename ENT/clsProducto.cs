using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENT
{
    public class clsProducto
    {
        #region Propiedades
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio_kilo { get; set; }
        public int Cantidad { get; set; }
        public decimal Impuesto { get; set; }
        public int ProveedorId { get; set; }
        #endregion
        #region Constructores
        public clsProducto()
        {
        }
        public clsProducto(int idProducto)
        {
            IdProducto = idProducto;
        }
        public clsProducto(int idProducto, string nombre, decimal precio, int cantidad, decimal impuesto)
        {
            IdProducto = idProducto;
            Nombre = nombre;
            Precio_kilo = precio;
            Cantidad = cantidad;
            Impuesto = impuesto;
        }
        #endregion
    }
}
