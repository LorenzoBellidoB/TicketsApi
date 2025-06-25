using DAL;
using Microsoft.EntityFrameworkCore;
using TicketsApi.Utils;

var builder = WebApplication.CreateBuilder(args);

//  1. Leer cadena de conexi�n desde DATABASE_URL (Render usa esa variable)
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

//  2. Aseg�rate de que sea compatible con PostgreSQL en Render (si es el caso)
connectionString = connectionString.Replace("postgres://", "Host=").Replace(":", ";").Replace("@", ";");

// Inyectar DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Servicios
builder.Services.AddScoped<clsProductosDAL>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//  3. Leer puerto de entorno (Render lo establece autom�ticamente)
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

// Middleware y Swagger (solo en desarrollo)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//  4. Probar conexi�n (usa la misma cadena de conexi�n)
var connectionTest = new TicketsApi.Utils.ConnectionTest(connectionString);
connectionTest.ProbarConexion();

app.Run();
