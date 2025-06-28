using ENT;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                                 .FirstOrDefaultAsync(pu => pu.IdProductoUnidad == id);
        }

        public async Task<List<clsProductoUnidad>> ObtenerProductoUnidadesPorProductoId(int productoId)
        {
            return await _context.ProductosUnidades
                                 .Include(pu => pu.Producto)
                                 .Where(pu => pu.IdProducto == productoId)
                                 .ToListAsync();
        }

        public async Task<bool> InsertarProductoUnidad(clsProductoUnidad productoUnidad)
        {
            _context.ProductosUnidades.Add(productoUnidad);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ActualizarProductoUnidad(clsProductoUnidad productoUnidad)
        {
            _context.ProductosUnidades.Update(productoUnidad);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EliminarProductoUnidad(int id)
        {
            var productoUnidad = await _context.ProductosUnidades.FindAsync(id);
            if (productoUnidad != null)
            {
                _context.ProductosUnidades.Remove(productoUnidad);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
