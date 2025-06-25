using DAL;
using Microsoft.EntityFrameworkCore;
using TicketsApi.Utils;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Leer DATABASE_URL o fallback a appsettings
var rawConnectionString = Environment.GetEnvironmentVariable("DATABASE_URL")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

string connectionString;

// Convertir DATABASE_URL si es estilo postgres://user:pass@host:port/db
if (rawConnectionString.StartsWith("postgres://"))
{
    var uri = new Uri(rawConnectionString);
    var userInfo = uri.UserInfo.Split(':');

    var npgsqlBuilder = new NpgsqlConnectionStringBuilder
    {
        Host = uri.Host,
        Port = uri.Port,
        Username = userInfo[0],
        Password = userInfo[1],
        Database = uri.AbsolutePath.Trim('/'),
        SslMode = SslMode.Require,
        TrustServerCertificate = true,
        PreferMultiplexing = false  // Evita IPv6 en algunos casos
    };

    connectionString = npgsqlBuilder.ToString();
}
else
{
    connectionString = rawConnectionString;
}

// Inyectar DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Servicios propios
builder.Services.AddScoped<clsProductosDAL>();

// Agregar controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Leer puerto de entorno en Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

// Activar Swagger en cualquier entorno
app.UseSwagger();
app.UseSwaggerUI();

// Redirección HTTPS (opcional en Render, puede causar errores si no se configura)
// app.UseHttpsRedirection();

app.UseAuthorization();

// Rutas
app.MapControllers();

// Probar conexión a base de datos (log interno)
var connectionTest = new ConnectionTest(connectionString);
connectionTest.ProbarConexion();

app.Run();
