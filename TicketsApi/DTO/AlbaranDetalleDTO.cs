using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsApi.DTO
{
    public class AlbaranDetalleDTO
    {
        public int IdAlbaranDetalle { get; set; }
       
        public int IdAlbaran { get; set; }
     
        public int IdProductoUnidad { get; set; }

        public AlbaranDetalleDTO()
        {
            
        }
        public AlbaranDetalleDTO(int idAlbaranDetalle, int idAlbaran, int idProductoUnidad)
        {
            IdAlbaranDetalle = idAlbaranDetalle;
            IdAlbaran = idAlbaran;
            IdProductoUnidad = idProductoUnidad;
        }
    }
}
