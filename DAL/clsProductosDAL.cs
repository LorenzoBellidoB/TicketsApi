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
            return await _context.Productos.ToListAsync();
        }

        public async Task<clsProducto> ObtenerProductoPorId(int id)
        {
            return await _context.Productos
                .FirstOrDefaultAsync(p => p.IdProducto == id);
        }

        public async Task<bool> InsertarProducto(clsProducto producto)
        {
            bool res = false;
            _context.Productos.Add(producto);
            res = await _context.SaveChangesAsync() > 0;
            return res;
        }

        public async Task<bool> ActualizarProducto(clsProducto producto)
        {
            bool res = false;
            _context.Productos.Update(producto);
            res = await _context.SaveChangesAsync() > 0;
            return res;
        }

        public async Task<bool> EliminarProducto(int id)
        {
            bool res = false;
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                res = await _context.SaveChangesAsync() > 0;
            }
            return res;
        }
    }
}
