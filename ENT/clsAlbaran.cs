using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ENT
{
    [Table("albaranes")]
    public class clsAlbaran : SoftDeletableEntity
    {
        #region Propiedades
        [Key]
        [Column("id")]
        public int IdAlbaran { get; set; }

        [Column("serie")]
        public string Serie { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Column("base_imponible")]
        public decimal BaseImponible { get; set; }

        [Column("importe_iva")]
        public decimal ImporteIVA { get; set; }

        [Column("importe")]
        public decimal Importe { get; set; }
        [Column("descuento")]
        public decimal Descuento { get; set; }

        [Column("descuento_p_pago")]
        public decimal DescuentoPPago { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Column("facturado")]
        public bool Facturado { get; set; }

        [Column("cliente_id")]
        public int IdCliente { get; set; }

        [ForeignKey("IdCliente")]
        public virtual clsCliente Cliente { get; set; }

        [Column("kilos")]
        public decimal Kilos { get; set; }

        [Column("empresa_id")]
        public int IdEmpresa { get; set; }

        [ForeignKey("IdEmpresa")]
        public virtual clsEmpresa Empresa { get; set; }

        [Column("dependiente_id")]
        public int IdDependiente { get; set; }

        [ForeignKey("IdDependiente")]
        public virtual clsDependiente Dependiente { get; set; }

        public virtual ICollection<clsTicket> Tickets { get; set; }

        public virtual ICollection<clsAlbaranDetalle> Detalles { get; set; } = new List<clsAlbaranDetalle>();
        #endregion

        #region Constructores
        public clsAlbaran()
        {
        }
        public clsAlbaran(int idAlbaran)
        {
            IdAlbaran = idAlbaran;
        }
        public clsAlbaran(int idAlbaran, string serie, DateTime fecha,decimal baseImponible,decimal importeIVA, decimal importe, decimal descuento, decimal descuentoPPago, string descripcion, bool facturado, int idCliente, decimal kilos, int idEmpresa, int idDependiente)
        {
            IdAlbaran = idAlbaran;
            Serie = serie;
            Fecha = fecha;
            Importe = importe;
            ImporteIVA = importeIVA;
            Descuento = descuento;
            DescuentoPPago = descuentoPPago;
            BaseImponible = baseImponible;
            Descripcion = descripcion;
            Facturado = facturado;
            IdCliente = idCliente;
            Kilos = kilos;
            IdEmpresa = idEmpresa;
            IdDependiente = idDependiente;
        }
        #endregion
    }
}
