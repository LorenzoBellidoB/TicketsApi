using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENT
{
    [Table("servicios")]
    public class clsServicio : SoftDeletableEntity
    {
        #region Propiedades
        [Key]
        [Column("id")]
        public int IdServicio { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("precio")]
        public decimal Precio { get; set; }

        [Column("empresa_id")]
        public int IdEmpresa { get; set; }

        [ForeignKey("IdEmpresa")]
        public virtual clsEmpresa Empresa { get; set; }
        #endregion
        #region Constructores
        public clsServicio() { }
        public clsServicio(int idServicio)
        {
            IdServicio = idServicio;
        }
        public clsServicio(int idServicio, string nombre, decimal precio)
        {
            IdServicio = idServicio;
            Nombre = nombre;
            Precio = precio;
        }
        #endregion
    }
}
