using System.ComponentModel.DataAnnotations.Schema;

namespace DTO
{
    public class AlbaranDetalleDTO
    {
        public int IdAlbaranDetalle { get; set; }
       
        public int IdAlbaran { get; set; }
     
        public int IdProductoUnidad { get; set; }

        public int IdServicio { get; set; }

        public AlbaranDetalleDTO()
        {
            
        }
        public AlbaranDetalleDTO( int idAlbaran, int idProductoUnidad)
        {
            IdAlbaran = idAlbaran;
            IdProductoUnidad = idProductoUnidad;
        }
    }
}
