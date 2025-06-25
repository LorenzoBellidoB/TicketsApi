using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ENT;
namespace DAL
{
    public class clsProductosDAL
    {
        public List<clsProducto> obtenerProductos()
        {
            List<clsProducto> productos = new List<clsProducto>();

            var connection = clsConexion.GetConnection();

            var command = new NpgsqlCommand("SELECT * FROM productos;", connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    productos.Add(new clsProducto
                    {
                        IdProducto = reader.GetInt32(0),
                        // Completa las demás propiedades...
                    });
                }
            }

            // No cerramos la conexión aquí, porque es estática y la cerramos con CloseConnection()

            return productos;
        }
    }

}
