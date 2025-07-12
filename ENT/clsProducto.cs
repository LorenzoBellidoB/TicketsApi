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
        public int IdProducto { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("precio_kilo")]
        public decimal Precio_kilo { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("impuesto")]
        public decimal Impuesto { get; set; }

        [Column("empresa_id")]
        public int IdEmpresa { get; set; }

        [ForeignKey("IdEmpresa")]
        public virtual clsEmpresa Empresa { get; set; }
        #endregion
        #region Constructores
        public clsProducto() { }
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
