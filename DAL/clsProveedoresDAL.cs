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
                .Include(p => p.Empresa)
                .FirstOrDefaultAsync(c => c.IdProveedor == id);
        }

        public async Task<List<clsProveedor>> ObtenerProveedoresPorIdEmpresa(int idEmpresa)
        {
            return await _context.Proveedores
                .Include(p => p.Empresa)
                .Where(p => p.IdEmpresa == idEmpresa)
                .ToListAsync();
        }

        public async Task<int> InsertarProveedor(clsProveedor proveedor)
        {
            int res = 0;
            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();
            res = proveedor.IdProveedor;
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
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null || proveedor.DeletedAt != DateTime.Parse("1111-01-01T00:00:00Z"))
                return false;

            proveedor.DeletedAt = DateTime.UtcNow;
            _context.Proveedores.Update(proveedor);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
