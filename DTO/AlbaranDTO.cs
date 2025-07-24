using System.ComponentModel.DataAnnotations.Schema;

namespace DTO
{
    public class AlbaranDTO
    {
        public int IdAlbaran { get; set; }
        public string Serie { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Importe { get; set; }
        public decimal ImporteIVA { get; set; }
        public decimal Descuento { get; set; }
        public decimal DescuentoPPago { get; set; }
        public string Descripcion { get; set; }
        public bool Facturado { get; set; }
        public int IdCliente { get; set; }
        public decimal Kilos { get; set; }
        public int IdEmpresa { get; set; }
        public int IdDependiente { get; set; }


        public AlbaranDTO()
        {
            
        }

        public AlbaranDTO(int idAlbaran, string serie, DateTime fecha, decimal importe,decimal importeIVA, decimal descuento, decimal descuentoPPago, string descripcion, bool facturado, int idCliente, decimal kilos, int idEmpresa, int idDependiente)
        {
            IdAlbaran = idAlbaran;
            Serie = serie;
            Fecha = fecha;
            Importe = importe;
            ImporteIVA = importeIVA;
            Descuento = descuento;
            DescuentoPPago = descuentoPPago;
            Descripcion = descripcion;
            Facturado = facturado;
            IdCliente = idCliente;
            Kilos = kilos;
            IdEmpresa = idEmpresa;
            IdDependiente = idDependiente;
        }
    }
}
