using DAL;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Linq;
using System.Net;
using TicketsApi.Utils;

var builder = WebApplication.CreateBuilder(args);

// 1) Leemos la URL de conexión (Render: DATABASE_URL, local: DefaultConnection)
var rawUrl = Environment.GetEnvironmentVariable("DATABASE_URL")
             ?? builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(rawUrl))
    throw new InvalidOperationException("Debe definir DATABASE_URL o DefaultConnection.");

// 2) Si es postgres:// lo parseamos; si no, usamos tal cual
string connectionString;
if (rawUrl.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase))
{
    var uri = new Uri(rawUrl);
    var parts = uri.UserInfo.Split(':', 2);
    var username = parts.ElementAtOrDefault(0) ?? "";
    var password = parts.ElementAtOrDefault(1) ?? "";
    var database = uri.AbsolutePath.Trim('/');

    // 2a) Forzamos IPv4 resolviendo nosotros mismos
    var addresses = Dns.GetHostAddresses(uri.Host);
    var ipv4Address = addresses.FirstOrDefault(a => a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                      ?? throw new InvalidOperationException($"No hay A record IPv4 para {uri.Host}");

    // 2b) Montamos la cadena con keywords seguros y sin propiedades obsoletas
    connectionString =
        $"Host={ipv4Address};" +
        $"Port={uri.Port};" +
        $"Username={username};" +
        $"Password={password};" +
        $"Database={database};" +
        // SSL obligatorio y confianza en el certificado autofirmado de Supabase
        $"Ssl Mode=Require;" +
        $"Trust Server Certificate=true;";
}
else
{
    // Cadena ya válida para Npgsql (entorno local, por ejemplo)
    connectionString = rawUrl;
}

// 3) Inyección de DbContext con retry-policy sencilla
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(connectionString, npg =>
        npg.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
    )
);

// 4) Registro de tus servicios, MVC y Swagger
builder.Services.AddScoped<clsProductosDAL>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 5) Ajuste de puerto que Render nos asigne
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

// 6) Middlewares
app.UseSwagger();
app.UseSwaggerUI();
// app.UseHttpsRedirection(); // En Render no suele hacer falta
app.UseAuthorization();
app.MapControllers();

// 7) Prueba de conexión al arrancar (mira los logs)
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider
                      .GetRequiredService<ILogger<Program>>();
    try
    {
        logger.LogInformation(">> Probando conexión a la base de datos…");
        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();
        logger.LogInformation(" Conexión OK.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, " No se pudo conectar:");
        // Si quieres abortar al primer fallo, descomenta:
        // throw;
    }
}

app.Run();
