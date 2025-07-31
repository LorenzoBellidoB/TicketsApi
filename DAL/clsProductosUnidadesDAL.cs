using ENT;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class clsProductosUnidadesDAL
    {
        private readonly AppDbContext _context;

        public clsProductosUnidadesDAL(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<clsProductoUnidad>> ObtenerProductoUnidades()
        {
            return await _context.ProductosUnidades
                                 .Include(pu => pu.Producto)
                                 .ToListAsync();
        }

        public async Task<clsProductoUnidad> ObtenerProductoUnidadPorId(int id)
        {
            return await _context.ProductosUnidades
                                 .Include(pu => pu.Producto)
                                 .Include(pu => pu.Empresa)
                                 .FirstOrDefaultAsync(pu => pu.IdProductoUnidad == id);
        }

        public async Task<List<clsProductoUnidad>> ObtenerProductoUnidadesPorProductoId(int productoId)
        {
            return await _context.ProductosUnidades
                                 .Include(pu => pu.Producto)
                                 .Where(pu => pu.IdProducto == productoId)
                                 .ToListAsync();
        }

        public async Task<List<clsProductoUnidad>> ObtenerProductoUnidadesPorEmpresaId(int empresaId)
        {
            return await _context.ProductosUnidades
                                 .Include(pu => pu.Producto)
                                 .Where(pu => pu.IdEmpresa == empresaId)
                                 .ToListAsync();
        }

        public async Task<bool> MarcarDisponible(int id)
        {
            var productoUnidad = await _context.ProductosUnidades.FindAsync(id);
            if (productoUnidad == null || !productoUnidad.Disponible)
                return false;

            productoUnidad.Disponible = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<clsProductoUnidad> InsertarProductoUnidad(clsProductoUnidad productoUnidad)
        {
            _context.ProductosUnidades.Add(productoUnidad);
            await _context.SaveChangesAsync();
            return productoUnidad;
        }

        public async Task<bool> ActualizarProductoUnidad(clsProductoUnidad unidad)
        {
            var original = await _context.ProductosUnidades
                .FirstOrDefaultAsync(pu => pu.IdProductoUnidad == unidad.IdProductoUnidad);

            if (original == null) return false;

            original.Peso = unidad.Peso;
            original.PrecioKilo = unidad.PrecioKilo;
            original.Etiqueta = unidad.Etiqueta;
            original.FechaEntrada = unidad.FechaEntrada;
            original.Disponible = unidad.Disponible;
            original.IdProducto = unidad.IdProducto;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EliminarProductoUnidad(int id)
        {
            var productoUnidad = await _context.ProductosUnidades.FindAsync(id);

            if (productoUnidad == null || productoUnidad.DeletedAt != SoftDeletableEntity.NotDeleted)
                return false;

            productoUnidad.DeletedAt = DateTime.UtcNow;
            _context.ProductosUnidades.Update(productoUnidad);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
