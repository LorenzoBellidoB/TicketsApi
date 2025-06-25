using Npgsql;

namespace TicketsApi.Utils
{
    public class ConnectionTest
    {
        private readonly string _connectionString;

        public ConnectionTest(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool ProbarConexion()
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                connection.Open();

                Console.WriteLine("✅ Conexión exitosa a la base de datos Supabase.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error al conectar con la base de datos:");
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
