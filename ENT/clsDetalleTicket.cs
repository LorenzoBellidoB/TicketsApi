using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENT
{
    [Table("tickets_detalles")]
    public class clsDetalleTicket : SoftDeletableEntity
    {
        #region Propiedades

        [Key]
        [Column("id")]
        public int IdDetalleTicket { get; set; }

        [Column("ticket_id")]
        public int IdTicket { get; set; }

        [ForeignKey("IdTicket")]
        public virtual clsTicket Ticket { get; set; }

        [Column("producto_unidad_id")]
        public int IdProductoUnidad { get; set; }

        [ForeignKey("IdProductoUnidad")]
        public virtual clsProductoUnidad ProductoUnidad { get; set; }

        [Column("servicio_id")]
        public int? IdServicio { get; set; }

        [ForeignKey("IdProductoUnidad")]
        public virtual clsServicio Servicio { get; set; }
        #endregion

        #region Constructores

        public clsDetalleTicket() { }

        public clsDetalleTicket(int idDetalleTicket)
        {
            IdDetalleTicket = idDetalleTicket;
        }

        public clsDetalleTicket(int idDetalleTicket, int idTicket, int idProductoUnidad, int idServicio)
        {
            IdDetalleTicket = idDetalleTicket;
            IdTicket = idTicket;
            IdProductoUnidad = idProductoUnidad;
            IdServicio = idServicio;
        }

        #endregion
    }
}
