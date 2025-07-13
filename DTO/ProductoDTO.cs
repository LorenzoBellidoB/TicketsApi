using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }

        public string Nombre { get; set; }

        public decimal Precio_kilo { get; set; }

        public int Cantidad { get; set; }

        public decimal Impuesto { get; set; }

        public int IdEmpresa { get; set; }

        public ProductoDTO()
        {
            
        }
        public ProductoDTO(int idProducto, string nombre, decimal precio, int cantidad, decimal impuesto)
        {
            IdProducto = idProducto;
            Nombre = nombre;
            Precio_kilo = precio;
            Cantidad = cantidad;
            Impuesto = impuesto;
        }
    }
}
