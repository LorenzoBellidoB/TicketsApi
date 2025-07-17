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
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public int IdDependiente { get; set; }
        public DateTime FechaCreado { get; set; }
        public DateTime FechaEntrega { get; set; }
        public int IdEmpresa { get; set; }
        public string Descripcion { get; set; }
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
