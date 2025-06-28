using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ENT
{
    [Table("productos_unidades")]
    public class clsProductoUnidad
    {
        #region Propiedades
        [Key]
        [Column("id")]
        public int IdProductoUnidad { get; set; }
        [Column("peso")]
        public decimal Peso { get; set; }
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
        #endregion
        #region Constructores
        public clsProductoUnidad()
        {
        }
        public clsProductoUnidad(int idProductoUnidad)
        {
            IdProductoUnidad = idProductoUnidad;
        }
        public clsProductoUnidad(int idProducto, decimal peso,string etiqueta, DateTime fechaEntrada, bool disponible)
        {
            IdProducto = idProducto;
            Peso = peso;
            Etiqueta = etiqueta;
            FechaEntrada = fechaEntrada;
            Disponible = disponible;
        }
        public clsProductoUnidad(int idProductoUnidad, int idProducto, decimal peso, string etiqueta, DateTime fechaEntrada, bool disponible)
        {
            IdProductoUnidad = idProductoUnidad;
            IdProducto = idProducto;
            Peso = peso;
            Etiqueta = etiqueta;
            FechaEntrada = fechaEntrada;
            Disponible = disponible;
        }
        #endregion
    }
}
