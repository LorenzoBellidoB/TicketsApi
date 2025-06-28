using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ENT
{
    [Table("tickets_detalles")]
    public class clsDetalleTicket
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

        [ForeignKey("IdProducto")]
        public virtual clsProductoUnidad ProductoUnidad { get; set; }

        [Column("cantidad")]
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
