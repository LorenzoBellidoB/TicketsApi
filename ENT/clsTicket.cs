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
        #endregion
        #region Constructores
        public clsTicket()
        {
        }
        public clsTicket(int idTicket)
        {
            IdTicket = idTicket;
        }
        public clsTicket(int idTicket, DateTime fecha, int idCliente, int idDependiente, int idEmpresa)
        {
            IdTicket = idTicket;
            Fecha = fecha;
            IdCliente = idCliente;
            IdDependiente = idDependiente;
            IdEmpresa = idEmpresa;
        }
        #endregion
    }
}
