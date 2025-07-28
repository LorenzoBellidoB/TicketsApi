using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ENT
{
    [Table("productos_unidades")]
    public class clsProductoUnidad : SoftDeletableEntity
    {
        #region Propiedades
        [Key]
        [Column("id")]
        public int IdProductoUnidad { get; set; }
        [Column("peso")]
        public decimal Peso { get; set; }

        [Column("precio_kilo")]
        public decimal PrecioKilo { get; set; }
        [Column("etiqueta")]
        public string Etiqueta { get; set; }
        [Column("fecha_entrada")]
        public DateTime FechaEntrada { get; set; }
        [Column("disponible")]
        public bool Disponible { get; set; }
        [Column("producto_id")]
        public int IdProducto { get; set; }

        [ForeignKey("IdProducto")]
        public virtual clsProducto Producto { get; set; }

        [Column("empresa_id")]
        public int IdEmpresa { get; set; }
        [ForeignKey("IdEmpresa")]
        public virtual clsEmpresa Empresa { get; set; } 
        #endregion
        #region Constructores
        public clsProductoUnidad()
        {
        }
        public clsProductoUnidad(int idProductoUnidad)
        {
            IdProductoUnidad = idProductoUnidad;
        }
        public clsProductoUnidad(int idProducto, decimal peso,decimal precio,string etiqueta, DateTime fechaEntrada, bool disponible, int idEmpresa)
        {
            IdProducto = idProducto;
            Peso = peso;
            PrecioKilo = precio;
            Etiqueta = etiqueta;
            FechaEntrada = fechaEntrada;
            Disponible = disponible;
            IdEmpresa = idEmpresa;
        }
        public clsProductoUnidad(int idProductoUnidad, int idProducto, decimal peso,decimal precio, string etiqueta, DateTime fechaEntrada, bool disponible, int idEmpresa)
        {
            IdProductoUnidad = idProductoUnidad;
            IdProducto = idProducto;
            Peso = peso;
            PrecioKilo = precio;
            Etiqueta = etiqueta;
            FechaEntrada = fechaEntrada;
            Disponible = disponible;
            IdEmpresa = idEmpresa;
        }
        #endregion
    }
}
