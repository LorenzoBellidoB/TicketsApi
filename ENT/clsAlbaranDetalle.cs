using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENT
{
    [Table("albaranes_detalles")]
    public class clsAlbaranDetalle : SoftDeletableEntity
    {
        #region Propiedades
        [Key]
        [Column("id")]
        public int IdAlbaranDetalle { get; set; }
        [Column("albaran_id")]
        public int IdAlbaran { get; set; }
        [Column("producto_unidad_id")]
        public int IdProductoUnidad { get; set; }

        [ForeignKey("IdAlbaran")]
        public virtual clsAlbaran Albaran { get; set; }

        [ForeignKey("IdProductoUnidad")]
        public virtual clsProductoUnidad ProductoUnidad { get; set; }

        [Column("servicio_id")]
        public int? IdServicio { get; set; }

        [ForeignKey("IdServicio")]
        public virtual clsServicio Servicio { get; set; }
        #endregion
        #region Constructores
        public clsAlbaranDetalle()
        {
        }
        public clsAlbaranDetalle(int idAlbaranDetalle)
        {
            IdAlbaranDetalle = idAlbaranDetalle;
        }

        public clsAlbaranDetalle(int idAlbaran, int idProductoUnidad)
        {
            IdAlbaran = idAlbaran;
            IdProductoUnidad = idProductoUnidad;
        }
        public clsAlbaranDetalle(int idAlbaranDetalle, int idAlbaran, int idProductoUnidad, int idServicio)
        {
            IdAlbaranDetalle = idAlbaranDetalle;
            IdAlbaran = idAlbaran;
            IdProductoUnidad = idProductoUnidad;
            IdServicio = idServicio;
        }
        #endregion
    }
}
