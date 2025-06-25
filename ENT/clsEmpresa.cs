using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENT
{
    [Table("empresas")]
    public class clsEmpresa
    {
        #region Propiedades
        [Key]
        [Column("id")]
        public int IdEmpresa { get; set; }
        [Column("nombre")]
        public string Nombre { get; set; }
        [Column("cif")]
        public string Cif {  get; set; }
        [Column("calle")]
        public string Calle { get; set; }
        [Column("codigo_postal")]
        public string Codigo_postal { get; set; }
        [Column("localidad")]
        public string Localidad { get; set; }
        [Column("provincia")]
        public string Provincia { get; set; }
        [Column("direccion")]
        public string Direccion { get; set; }


        #endregion

        #region Constructores

        public clsEmpresa()
        {
        }

        public clsEmpresa(int idEmpresa)
        {
            IdEmpresa = idEmpresa;
        }

        public clsEmpresa(int idEmpresa, string nombre, string cif, string calle, string codigo_postal, string localidad, string provincia, string direccion)
        {
            IdEmpresa = idEmpresa;
            Nombre = nombre;
            Cif = cif;
            Calle = calle;
            Codigo_postal = codigo_postal;
            Localidad = localidad;
            Provincia = provincia;
            Direccion = direccion;
        }

        #endregion
    }
}
