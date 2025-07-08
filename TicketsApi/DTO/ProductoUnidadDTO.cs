using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsApi.DTO
{
    public class ProductoUnidadDTO
    {
        public int IdProductoUnidad { get; set; }

        public decimal Peso { get; set; }

        public decimal PrecioKilo { get; set; }

        public string Etiqueta { get; set; }

        public DateTime FechaEntrada { get; set; }

        public bool Disponible { get; set; }

        public int IdProducto { get; set; }

        public ProductoUnidadDTO()
        {
        }

        public ProductoUnidadDTO(decimal peso,decimal precio, string etiqueta, DateTime fechaEntrada, bool disponible, int idProducto)
        {
            Peso = peso;
            PrecioKilo = precio;
            Etiqueta = etiqueta;
            FechaEntrada = fechaEntrada;
            Disponible = disponible;
            IdProducto = idProducto;
        }

        public ProductoUnidadDTO(int idProductoUnidad, decimal peso, decimal precio,string etiqueta, DateTime fechaEntrada, bool disponible, int idProducto)
        {
            IdProductoUnidad = idProductoUnidad;
            Peso = peso;
            PrecioKilo = precio;
            Etiqueta = etiqueta;
            FechaEntrada = fechaEntrada;
            Disponible = disponible;
            IdProducto = idProducto;
        }
    }
}
