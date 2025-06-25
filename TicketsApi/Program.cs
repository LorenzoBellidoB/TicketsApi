using DAL;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Net;
using System.Net.Sockets;
using TicketsApi.Utils;

var builder = WebApplication.CreateBuilder(args);

// 1) Leemos la URL de conexión (DATABASE_URL en Render, DefaultConnection en local)
var rawUrl = Environment.GetEnvironmentVariable("DATABASE_URL")
             ?? builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(rawUrl))
    throw new InvalidOperationException("Debe definir DATABASE_URL o DefaultConnection.");

// 2) Creamos un NpgsqlConnectionStringBuilder a partir de rawUrl
var csb = new NpgsqlConnectionStringBuilder();

// Si viene en formato postgres://user:pass@host:port/dbname, lo parseamos
if (rawUrl.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase))
{
    var uri = new Uri(rawUrl);
    var up = uri.UserInfo.Split(':', 2);

    csb.Username = up.ElementAtOrDefault(0) ?? "";
    csb.Password = up.ElementAtOrDefault(1) ?? "";
    csb.Database = uri.AbsolutePath.Trim('/');

    // ¡Aquí resolvemos y usamos sólo la IPv4!
    var addresses = Dns.GetHostAddresses(uri.Host);
    var ipv4 = addresses.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork)
               ?? throw new InvalidOperationException($"No hay A record IPv4 para {uri.Host}");
    Console.WriteLine($"[DEBUG] Usando IP v4: {ipv4}");
    csb.Host = ipv4.ToString();

    csb.Port = uri.Port;
}
else
{
    // Si rawUrl ya es una cadena Npgsql estándar
    csb.ConnectionString = rawUrl;
}

// 3) Forzamos SSL/TLS y confiamos certificado autofirmado
csb.SslMode = SslMode.Require;
csb.TrustServerCertificate = true;

// DEBUG: Ver cadena final
Console.WriteLine($"[DEBUG] ConnectionString final: {csb.ConnectionString}");

// 4) Registramos el DbContext con política de reintentos
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseNpgsql(csb.ConnectionString, npg =>
        npg.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
    )
);

// 5) Resto de servicios, controllers y swagger
builder.Services.AddScoped<clsProductosDAL>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 6) Configuramos el puerto que Render asigne
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

// 7) Middlewares
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();

// 8) Test de conexión al arranque (logs de Render)
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        logger.LogInformation(" Probando conexión a la base de datos…");
        using var conn = new NpgsqlConnection(csb.ConnectionString);
        conn.Open();
        logger.LogInformation(" Conexión establecida correctamente.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, " Error al conectar con la base de datos:");
    }
}

app.Run();
