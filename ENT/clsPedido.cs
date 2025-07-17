using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENT
{
    [Table("pedidos")]
    public class clsPedido : SoftDeletableEntity
    {
        #region Propiedades
        [Key]
        [Column("id")]
        public int IdPedido { get; set; }
        [Column("cliente_id")]
        public int IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public virtual clsCliente Cliente { get; set; }
        [Column("dependiente_id")]
        public int IdDependiente { get; set; }
        [ForeignKey("IdDependiente")]
        public virtual clsDependiente Dependiente { get; set; }
        [Column("fecha_creado")]
        public DateTime FechaCreado { get; set; }
        [Column("fecha_entrega")]
        public DateTime FechaEntrega { get; set; }
        [Column("empresa_id")]
        public int IdEmpresa { get; set; }
        [ForeignKey("IdEmpresa")]
        public virtual clsEmpresa Empresa { get; set; }
        [Column("descripcion")]
        public string Descripcion { get; set; }
        [Column("entregado")]
        public bool Entregado { get; set; }
        #endregion

        #region Constructores
        public clsPedido() { }
        public clsPedido(int idPedido, int idCliente, int idDependiente, DateTime fechaCreado, DateTime fechaEntrega, int idEmpresa, string descripcion)
        {
            IdPedido = idPedido;
            IdCliente = idCliente;
            IdDependiente = idDependiente;
            FechaCreado = fechaCreado;
            FechaEntrega = fechaEntrega;
            IdEmpresa = idEmpresa;
            Descripcion = descripcion;
        }
        #endregion
    }
}
