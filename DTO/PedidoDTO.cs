using ENT;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PedidoDTO
    {
        [Column("id")]
        public int IdPedido { get; set; }
        [Column("cliente_id")]
        public int IdCliente { get; set; }
        [Column("dependiente_id")]
        public int IdDependiente { get; set; }
        [Column("fecha_creado")]
        public DateTime FechaCreado { get; set; }
        [Column("fecha_entrega")]
        public DateTime FechaEntrega { get; set; }
        [Column("empresa_id")]
        public int IdEmpresa { get; set; }
        [Column("descripcion")]
        public string Descripcion { get; set; }
        [Column("entregado")]
        public bool Entregado { get; set; }

        public PedidoDTO() { }

        public PedidoDTO(int idPedido, int idCliente, int idDependiente, DateTime fechaCreado, DateTime fechaEntrega, int idEmpresa, string descripcion)
        {
            IdPedido = idPedido;
            IdCliente = idCliente;
            IdDependiente = idDependiente;
            FechaCreado = fechaCreado;
            FechaEntrega = fechaEntrega;
            IdEmpresa = idEmpresa;
            Descripcion = descripcion;
        }
    }
}
