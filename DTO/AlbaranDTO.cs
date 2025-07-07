namespace DTO
{
    public class AlbaranDTO
    {
        public int IdAlbaran { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Importe { get; set; }
        public string Descripcion { get; set; }
        public bool Facturado { get; set; }
        public int IdCliente { get; set; }
        public decimal Kilos { get; set; }
        public int IdEmpresa { get; set; }
        public int IdDependiente { get; set; }


        public AlbaranDTO()
        {
            Serie = "";
            Numero = "";
            Fecha = DateTime.Now;
            Importe = 0;
            Descripcion = "";
            Facturado = false;
            IdCliente = 0;
            Kilos = 0;
            IdEmpresa = 0;
            IdDependiente = 0;
        }
    }
}
