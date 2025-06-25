using DAL;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;             // <- Necesario para AddressFamily
using TicketsApi.Utils;

var builder = WebApplication.CreateBuilder(args);

// 1) Leer la URL de conexión de Render/Supabase o, en local, DefaultConnection
var rawConnectionString = Environment.GetEnvironmentVariable("DATABASE_URL")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(rawConnectionString))
    throw new InvalidOperationException("No se ha encontrado DATABASE_URL ni DefaultConnection.");

// 2) Si viene en formato postgres://... lo parseamos y forzamos IPv4 + SSL/TLS
string connectionString;
if (rawConnectionString.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase))
{
    var uri = new Uri(rawConnectionString);
    var userPass = uri.UserInfo.Split(':', 2);
    var dbName = uri.AbsolutePath.Trim('/');

    // 2a) Resolución DNS a IPv4
    var dnsAddresses = Dns.GetHostAddresses(uri.Host);
    var ipv4Address = dnsAddresses.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork)
        ?? throw new InvalidOperationException($"No se halló dirección IPv4 para '{uri.Host}'.");

    // 2b) Montamos la cadena manualmente, usando el keyword "Trust Server Certificate"
    connectionString =
        $"Host={ipv4Address};" +
        $"Port={uri.Port};" +
        $"Username={userPass.ElementAtOrDefault(0)};" +
        $"Password={userPass.ElementAtOrDefault(1)};" +
        $"Database={dbName};" +
        // SSL/TLS obligatorio y confianza en el certificado autofirmado de Supabase:
        $"Ssl Mode=Require;" +
        $"Trust Server Certificate=true";
}
else
{
    // 3) Ya es una cadena Npgsql normal (por ejemplo en desarrollo local)
    connectionString = rawConnectionString;
}

// 4) Inyectamos el DbContext con retry-policy
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseNpgsql(connectionString, npgOptions =>
        npgOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorCodesToAdd: null
        )
    )
);

// 5) Registro de servicios y Swagger
builder.Services.AddScoped<clsProductosDAL>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 6) Forzar el puerto que Render asigne
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

// 7) Middleware
app.UseSwagger();
app.UseSwaggerUI();
// app.UseHttpsRedirection(); // No necesario en Render
app.UseAuthorization();
app.MapControllers();

// 8) Test de conexión al arrancar (sale en Logs)
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        logger.LogInformation("Probando conexión a la base de datos...");
        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();
        logger.LogInformation(" Conexión establecida correctamente.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, " Error al conectar con la base de datos:");
        // Si quieres detener la app en fallo crítico, descomenta:
        // throw;
    }
}

app.Run();
