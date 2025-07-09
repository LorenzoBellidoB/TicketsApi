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

        [Column("correo")]
        public string Correo { get; set; }

        [Column("telefono")]
        public long Telefono { get; set; }

        [Column("dni")]
        public string Dni { get; set; }

        [Column("empresa_id")]
        public int IdEmpresa { get; set; }

        [ForeignKey("IdEmpresa")]
        public virtual clsEmpresa Empresa { get; set; }
        #endregion

        #region Constructores
        public clsDependiente()
        {
        }
        public clsDependiente(int idDependiente)
        {
            IdDependiente = idDependiente;
        }
        public clsDependiente(int idDependiente, string nombre, string correo, long telefono, string dni, int idEmpresa)
        {
            IdDependiente = idDependiente;
            Nombre = nombre;
            Correo = correo;
            Telefono = telefono;
            Dni = dni;
            IdEmpresa = idEmpresa;
        }
        #endregion
    }
}
