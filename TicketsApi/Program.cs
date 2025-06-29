using DAL;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Net;
using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);

// 1) Leemos la URL de conexión (DATABASE_URL en Render, DefaultConnection en local)
var rawUrl = Environment.GetEnvironmentVariable("DATABASE_URL")
             ?? builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(rawUrl))
    throw new InvalidOperationException("Debe definir DATABASE_URL o DefaultConnection.");

// 2) Creamos el connection string builder
var csb = new NpgsqlConnectionStringBuilder();

try
{
    // Acepta postgres:// y postgresql://
    var uri = new Uri(rawUrl.Replace("postgresql://", "postgres://"));
    var up = uri.UserInfo.Split(':', 2);

    csb.Username = up.ElementAtOrDefault(0) ?? "";
    csb.Password = up.ElementAtOrDefault(1) ?? "";
    csb.Database = uri.AbsolutePath.Trim('/');

    //  Forzamos IP v4
    var addresses = Dns.GetHostAddresses(uri.Host);
    var ipv4 = addresses.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork)
               ?? throw new InvalidOperationException($"No hay A record IPv4 para {uri.Host}");
    Console.WriteLine($"[DEBUG] Usando IP v4: {ipv4}");
    csb.Host = ipv4.ToString();
    csb.Port = uri.Port;
}
catch
{
    // Si rawUrl ya es una cadena estándar tipo "Host=..."
    csb.ConnectionString = rawUrl;
}

// 3) Seguridad: TLS y certificado
csb.SslMode = SslMode.Require;
csb.TrustServerCertificate = true;

// DEBUG: Ver cadena final
Console.WriteLine($"[DEBUG] ConnectionString final: {csb.ConnectionString}");

// 4) DbContext con reintentos
builder.Services.AddDbContext<DAL.AppDbContext>(opts =>
    opts.UseNpgsql(csb.ConnectionString, npg =>
        npg.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
    )
);


// 5) Servicios y Swagger
builder.Services.AddScoped<clsProductosDAL, clsProductosDAL>();
builder.Services.AddScoped<clsTicketsDAL, clsTicketsDAL>();
builder.Services.AddScoped<clsDetalleTicketsDAL, clsDetalleTicketsDAL>();
builder.Services.AddScoped<clsAlbaranesDAL, clsAlbaranesDAL>();
builder.Services.AddScoped<clsEmpresasDAL, clsEmpresasDAL>();
builder.Services.AddScoped<clsDependientesDAL, clsDependientesDAL>();
builder.Services.AddScoped<clsClientesDAL, clsClientesDAL>();
builder.Services.AddScoped<clsProveedoresDAL, clsProveedoresDAL>();
builder.Services.AddScoped<clsAlbaranesDetallesDAL, clsAlbaranesDetallesDAL>();
builder.Services.AddScoped<clsProductosUnidadesDAL, clsProductosUnidadesDAL>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 6) Puerto dinámico (Render)
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

// 7) Middlewares
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();

// 8) Test de conexión al arranque
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
