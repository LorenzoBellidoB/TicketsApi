using ENT;
using Microsoft.EntityFrameworkCore;
using System;
using TicketsApi.Utils;

namespace DAL
{
    public class clsProductosDAL
    {
        private readonly AppDbContext _context;

        public clsProductosDAL(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<clsProducto>> obtenerProductos()
        {
            return await _context.Productos.ToListAsync();
        }
    }
}
