using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENT
{
    [Table("productos")] 
    public class clsProducto
    {
        #region Propiedades
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nombre")]
        public string Nombre { get; set; }
        [Column("precio_kilo")]
        public decimal Precio_kilo { get; set; }
        [Column("cantidad")]
        public int Cantidad { get; set; }
        [Column("impuesto")]
        public decimal Impuesto { get; set; }
        [Column("proveedor_id")]
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
