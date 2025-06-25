using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENT
{
    [Table("dependientes")]
    public class clsDependiente
    {
        #region Propiedades
        [Key]
        [Column("id")]
        public int IdDependiente { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("empresa_id")]
        public int IdEmpresa { get; set; }

        [ForeignKey("IdEmpresa")]
        public virtual clsEmpresa Empresa { get; set; }

        public virtual ICollection<clsTicket> Tickets { get; set; }
        public virtual ICollection<clsAlbaran> Albaranes { get; set; }



        #endregion

        #region Constructores
        public clsDependiente()
        {
        }
        public clsDependiente(int idDependiente)
        {
            IdDependiente = idDependiente;
        }
        public clsDependiente(int idDependiente, string nombre, int idEmpresa)
        {
            IdDependiente = idDependiente;
            Nombre = nombre;
            IdEmpresa = idEmpresa;
        }
        #endregion
    }
}
