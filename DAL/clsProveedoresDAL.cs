using ENT;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class clsProveedoresDAL
    {
        private readonly AppDbContext _context;

        public clsProveedoresDAL(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<clsProveedor>> ObtenerProveedores()
        {
            return await _context.Proveedores.ToListAsync();
        }

        public async Task<clsProveedor> ObtenerProveedorPorId(int id)
        {
            return await _context.Proveedores
                .FirstOrDefaultAsync(c => c.IdProveedor == id);
        }

        public async Task<List<clsProveedor>> ObtenerProveedoresPorIdEmpresa(int idEmpresa)
        {
            return await _context.Proveedores
                .Include(p => p.Empresa)
                .Where(p => p.IdEmpresa == idEmpresa)
                .ToListAsync();
        }

        public async Task<bool> InsertarProveedor(clsProveedor proveedor)
        {
            bool res = false;
            _context.Proveedores.Add(proveedor);
            res = await _context.SaveChangesAsync() > 0;
            return res;
        }

        public async Task<bool> ActualizarProveedor(clsProveedor proveedor)
        {
            bool res = false;
            _context.Proveedores.Update(proveedor);
            res = await _context.SaveChangesAsync() > 0;
            return res;
        }

        public async Task<bool> EliminarProveedor(int id)
        {
            bool res = false;
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor != null)
            {
                _context.Proveedores.Remove(proveedor);
                res = await _context.SaveChangesAsync() > 0;
            }
            return res;
        }
    }
}
