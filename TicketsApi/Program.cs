using DAL;
using Microsoft.EntityFrameworkCore;
using TicketsApi.Utils;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Leer DATABASE_URL o fallback
var rawConnectionString = Environment.GetEnvironmentVariable("DATABASE_URL")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

string connectionString;

// Convertir DATABASE_URL si viene en formato postgres://user:pass@host:port/db
if (!string.IsNullOrEmpty(rawConnectionString) && rawConnectionString.StartsWith("postgres://"))
{
    var uri = new Uri(rawConnectionString);
    var userInfo = uri.UserInfo.Split(':', 2); // Split una sola vez

    var npgsqlBuilder = new NpgsqlConnectionStringBuilder
    {
        Host = uri.Host,
        Port = uri.Port,
        Username = userInfo.Length > 0 ? userInfo[0] : "",
        Password = userInfo.Length > 1 ? userInfo[1] : "",
        Database = uri.AbsolutePath.Trim('/'),
        SslMode = SslMode.Require
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

// Swagger y controladores
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Puerto para Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

// Swagger activo siempre
app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection(); // No necesario en Render
app.UseAuthorization();
app.MapControllers();

// Probar conexión
var connectionTest = new ConnectionTest(connectionString);
connectionTest.ProbarConexion();

app.Run();
