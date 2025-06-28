using ENT;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class clsProductoUnidadDAL
    {
        private readonly AppDbContext _context;

        public clsProductoUnidadDAL(AppDbContext context)
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
                                 .FirstOrDefaultAsync(pu => pu.IdProductoUnidad == id);
        }

        public async Task<bool> InsertarProductoUnidad(clsProductoUnidad productoUnidad)
        {
            bool res = false;
            _context.ProductosUnidades.Add(productoUnidad);
            res = await _context.SaveChangesAsync() > 0;
            return res;
        }

        public async Task<bool> ActualizarProductoUnidad(clsProductoUnidad productoUnidad)
        {
            bool res = false;
            _context.ProductosUnidades.Update(productoUnidad);
            res = await _context.SaveChangesAsync() > 0;
            return res;
        }

        public async Task<bool> EliminarProductoUnidad(int id)
        {
            bool res = false;
            var productoUnidad = await _context.ProductosUnidades.FindAsync(id);
            if (productoUnidad != null)
            {
                _context.ProductosUnidades.Remove(productoUnidad);
                res = await _context.SaveChangesAsync() > 0;
            }
            return res;
        }
    }
}
