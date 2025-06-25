using DAL;
using Microsoft.EntityFrameworkCore;
using TicketsApi.Utils;

var builder = WebApplication.CreateBuilder(args);

// Cargar cadena de conexi�n desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Inyectar DbContext (si est�s usando EF Core)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Registrar servicios de tu propia capa DAL (opcional si usas inyecci�n de dependencias)
builder.Services.AddScoped<clsProductosDAL>();

// Agregar servicios de controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuraci�n de entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middlewares
app.UseHttpsRedirection();
app.UseAuthorization();

// Mapear controladores
app.MapControllers();

var connectionTest = new TicketsApi.Utils.ConnectionTest(
    builder.Configuration.GetConnectionString("DefaultConnection")
);
connectionTest.ProbarConexion();


app.Run();
