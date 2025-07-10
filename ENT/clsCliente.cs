using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENT
{
    [Table("clientes")]
    public class clsCliente
    {
        #region Propiedades
        [Key]
        [Column("id")]
        public int IdCliente { get; set; }
        [Column("nombre")]
        public string Nombre { get; set; }
        [Column("correo")]
        public string Correo { get; set; }
        [Column("telefono")]
        public string Telefono { get; set; }
        [Column("cif")]
        public string Cif { get; set; }
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

        [Column("empresa_id")]
        public int IdEmpresa { get; set; }

        [ForeignKey("IdEmpresa")]
        public virtual clsEmpresa Empresa { get; set; }
        #endregion

        #region Constructores
        public clsCliente()
        {
        }
        public clsCliente(int idCliente)
        {
            IdCliente = idCliente;
        }

        public clsCliente(int idCliente, string nombre, string correo, string telefono, string calle, string codigo_postal, string localidad, string provincia)
        {
            IdCliente = idCliente;
            Nombre = nombre;
            Correo = correo;
            Telefono = telefono;
            Calle = calle;
            Codigo_postal = codigo_postal;
            Localidad = localidad;
            Provincia = provincia;
        }

        public clsCliente(int idCliente, string nombre, string correo, string telefono, string calle, string codigo_postal, string localidad, string provincia, string direccion)
        {
            IdCliente = idCliente;
            Nombre = nombre;
            Correo = correo;
            Telefono = telefono;
            Calle = calle;
            Codigo_postal = codigo_postal;
            Localidad = localidad;
            Provincia = provincia;
            Direccion = direccion;
        }

        #endregion
    }
}
