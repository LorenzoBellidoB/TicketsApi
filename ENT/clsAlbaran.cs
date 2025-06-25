using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENT
{
    public class clsAlbaran
    {
        #region Propiedades
        public int IdAlbaran { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public DateTime Fecha { get; set; }

        public decimal Importe { get; set; }
        public string Descripcion { get; set; }
        public int IdCliente { get; set; }
        public decimal Kilos { get; set; }
        public int IdEmpresa { get; set; }
        public int IdDependiente { get; set; }

        #endregion

        #region Constructores
        public clsAlbaran()
        {
        }
        public clsAlbaran(int idAlbaran)
        {
            IdAlbaran = idAlbaran;
        }
        public clsAlbaran(int idAlbaran, string serie, string numero, DateTime fecha, decimal importe, string descripcion, int idCliente, decimal kilos, int idEmpresa, int idDependiente)
        {
            IdAlbaran = idAlbaran;
            Serie = serie;
            Numero = numero;
            Fecha = fecha;
            Importe = importe;
            Descripcion = descripcion;
            IdCliente = idCliente;
            Kilos = kilos;
            IdEmpresa = idEmpresa;
            IdDependiente = idDependiente;
        }
        #endregion
    }
}
