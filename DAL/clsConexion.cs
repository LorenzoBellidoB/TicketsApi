using Npgsql;

public static class clsConexion
{
    private static NpgsqlConnection _connection;

    private static string _connectionString = "Host=db.dpxxctjanvcusyzivlfk.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=lwtDSZDnEbEo2oHa;Ssl Mode=Require;Trust Server Certificate=true;Pooling=false";

    public static NpgsqlConnection GetConnection()
    {
        if (_connection == null)
        {
            _connection = new NpgsqlConnection(_connectionString);
            _connection.Open();
        }
        else if (_connection.State != System.Data.ConnectionState.Open)
        {
            _connection.Open();
        }

        return _connection;
    }

    public static void CloseConnection()
    {
        if (_connection != null && _connection.State != System.Data.ConnectionState.Closed)
        {
            _connection.Close();
            _connection.Dispose();
            _connection = null;
        }
    }
}
