using ENT;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    public class clsProductosDAL
    {
        private readonly AppDbContext _context;

        public clsProductosDAL(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<clsProducto>> ObtenerProductos()
        {
            return await _context.Productos.Include(p => p.Empresa).ToListAsync();
        }

        public async Task<clsProducto> ObtenerProductoPorId(int id)
        {
            return await _context.Productos.Include(p => p.Empresa)
                .FirstOrDefaultAsync(p => p.IdProducto == id);
        }

        public async Task<List<clsProducto>> ObtenerProductosPorIdEmpresa(int idEmpresa)
        {
            return await _context.Productos
                .Include(p => p.Empresa)
                .Where(p => p.Empresa.IdEmpresa == idEmpresa)
                .ToListAsync();
        }

        public async Task<int> InsertarProducto(clsProducto producto)
        {
            int res = 0;
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            res = producto.IdProducto;
            return res;
        }

        public async Task<bool> ActualizarProducto(clsProducto producto)
        {
            var productoOriginal = await _context.Productos
                .FirstOrDefaultAsync(p => p.IdProducto == producto.IdProducto);

            if (productoOriginal == null)
                return false;

            productoOriginal.Nombre = producto.Nombre;
            productoOriginal.Precio_kilo = producto.Precio_kilo;
            productoOriginal.Cantidad = producto.Cantidad;
            productoOriginal.Impuesto = producto.Impuesto;
            productoOriginal.IdEmpresa = producto.IdEmpresa;

            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> EliminarProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null || producto.DeletedAt != DateTime.SpecifyKind(DateTime.Parse("1111-01-01T00:00:00Z"), DateTimeKind.Utc))
                return false;

            producto.DeletedAt = DateTime.UtcNow;
            _context.Productos.Update(producto);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
