using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;  // <-- necesario para [Table]

namespace ENT
{
    [Table("productos")] // Aquí indicas el nombre exacto de la tabla en la base de datos (minúsculas)
    public class clsProducto
    {
        #region Propiedades
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio_kilo { get; set; }
        public int Cantidad { get; set; }
        public decimal Impuesto { get; set; }
        public int ProveedorId { get; set; }
        #endregion
        #region Constructores
        public clsProducto() { }
        public clsProducto(int idProducto)
        {
            Id = idProducto;
        }
        public clsProducto(int idProducto, string nombre, decimal precio, int cantidad, decimal impuesto)
        {
            Id = idProducto;
            Nombre = nombre;
            Precio_kilo = precio;
            Cantidad = cantidad;
            Impuesto = impuesto;
        }
        #endregion
    }
}
