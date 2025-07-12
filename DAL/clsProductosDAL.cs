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
            return await _context.Productos.Include(p => p.Empresa)
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
