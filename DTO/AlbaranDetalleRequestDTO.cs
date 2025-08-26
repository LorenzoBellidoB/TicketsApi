using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AlbaranDetalleRequestDTO
    {
        public List<ProductoUnidadDTO> Unidades { get; set; } = new();
        public List<ServicioDTO>? Servicios { get; set; }
    }
}
