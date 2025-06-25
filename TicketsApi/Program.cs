using DAL;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using TicketsApi.Utils;

var builder = WebApplication.CreateBuilder(args);

// 1) Leemos DATABASE_URL de Render o, en desarrollo, la conexión por defecto
var rawConnectionString = Environment.GetEnvironmentVariable("DATABASE_URL")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(rawConnectionString))
    throw new InvalidOperationException("No se ha encontrado la cadena de conexión (DATABASE_URL o DefaultConnection).");

string connectionString;

// 2) Si viene en formato Heroku/Supabase (postgres://user:pass@host:port/dbname), la convertimos:
if (rawConnectionString.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase))
{
    var uri = new Uri(rawConnectionString);
    var userInfo = uri.UserInfo.Split(':', 2);

    var npgsqlBuilder = new NpgsqlConnectionStringBuilder
    {
        Host = uri.Host,
        Port = uri.Port,
        Username = userInfo.ElementAtOrDefault(0) ?? "",
        Password = userInfo.ElementAtOrDefault(1) ?? "",
        Database = uri.AbsolutePath.Trim('/'),
        SslMode = SslMode.Require,
        TrustServerCertificate = true  // <- IMPORTANTE en Supabase
    };

    connectionString = npgsqlBuilder.ToString();
}
else
{
    // Ya es una cadena Npgsql válida (ej. en local)
    connectionString = rawConnectionString;
}

// 3) Inyectamos el DbContext con retry-policy
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString, npgsqlOptions =>
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorCodesToAdd: null
        )
    )
);

// Servicios propios
builder.Services.AddScoped<clsProductosDAL>();

// Swagger y controladores
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4) Configuramos el puerto que Render nos asigne
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

// Middleware
app.UseSwagger();
app.UseSwaggerUI();
// app.UseHttpsRedirection(); // En Render no es necesario
app.UseAuthorization();
app.MapControllers();

// 5) Probar conexión al arrancar
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        logger.LogInformation("Probando conexión a la base de datos...");
        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();
        logger.LogInformation("Conexión establecida correctamente.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, " Error al conectar con la base de datos:");
        // Si lo quieres, puedes detener la app:
        // throw;
    }
}

app.Run();
