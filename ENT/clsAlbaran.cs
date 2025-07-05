using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ENT
{
    [Table("albaranes")]
    public class clsAlbaran
    {
        #region Propiedades
        [Key]
        [Column("id")]
        public int IdAlbaran { get; set; }

        [Column("serie")]
        public string Serie { get; set; }

        [Column("numero")]
        public string Numero { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Column("importe")]
        public decimal Importe { get; set; }

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
        public clsAlbaran(int idAlbaran, string serie, string numero, DateTime fecha, decimal importe, string descripcion, bool facturado, int idCliente, decimal kilos, int idEmpresa, int idDependiente)
        {
            IdAlbaran = idAlbaran;
            Serie = serie;
            Numero = numero;
            Fecha = fecha;
            Importe = importe;
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
