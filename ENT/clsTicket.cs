using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENT
{
    [Table("tickets")]
    public class clsTicket
    {
        #region Propiedades

        [Key]
        [Column("id")]
        public int IdTicket { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Column("importe")]
        public decimal Importe { get; set; }

        [Column("base_imponible")]
        public decimal BaseImponible { get; set; }

        [Column("importe_iva")]
        public decimal ImporteIVA { get; set; }

        [Column("cliente_id")]
        public int IdCliente { get; set; }

        [ForeignKey("IdCliente")]
        public virtual clsCliente Cliente { get; set; }

        [Column("dependiente_id")]
        public int IdDependiente { get; set; }

        [ForeignKey("IdDependiente")]
        public virtual clsDependiente Dependiente { get; set; }

        [Column("empresa_id")]
        public int IdEmpresa { get; set; }

        [ForeignKey("IdEmpresa")]
        public virtual clsEmpresa Empresa { get; set; }

        [Column("albaran_id")]
        public int IdAlbaran { get; set; }

        [ForeignKey("IdAlbaran")]
        public virtual clsAlbaran Albaran { get; set; }

        public virtual ICollection<clsDetalleTicket> Detalles { get; set; } = new List<clsDetalleTicket>();

        #endregion

        #region Constructores

        public clsTicket() { }

        public clsTicket(int idTicket)
        {
            IdTicket = idTicket;
        }

        public clsTicket(int idTicket, DateTime fecha, decimal importe, decimal baseImponible, decimal importeIVA, int idCliente, int idDependiente, int idEmpresa, int idAlbaran)
        {
            IdTicket = idTicket;
            Fecha = fecha;
            Importe = importe;
            BaseImponible = baseImponible;
            ImporteIVA = importeIVA;
            IdCliente = idCliente;
            IdDependiente = idDependiente;
            IdEmpresa = idEmpresa;
            IdAlbaran = idAlbaran;
        }

        #endregion
    }
}
