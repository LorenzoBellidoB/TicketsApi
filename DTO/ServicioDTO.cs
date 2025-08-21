using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ServicioDTO
    {
        public int IdServicio { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int IdEmpresa { get; set; }
        public ServicioDTO()
        {
        }
        public ServicioDTO(int idServicio, string nombre, decimal precio)
        {
            IdServicio = idServicio;
            Nombre = nombre;
            Precio = precio;
        }
    }
}
