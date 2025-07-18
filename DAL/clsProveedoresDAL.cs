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
            var original = await _context.Proveedores
                .FirstOrDefaultAsync(p => p.IdProveedor == proveedor.IdProveedor);

            if (original == null) return false;

            original.Nombre = proveedor.Nombre;
            original.Cif = proveedor.Cif;
            original.Telefono = proveedor.Telefono;
            original.Correo = proveedor.Correo;
            original.Calle = proveedor.Calle;
            original.Codigo_postal = proveedor.Codigo_postal;
            original.Localidad = proveedor.Localidad;
            original.Provincia = proveedor.Provincia;
            original.IdEmpresa = proveedor.IdEmpresa;

            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> EliminarProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null || proveedor.DeletedAt != DateTime.SpecifyKind(DateTime.Parse("1111-01-01T00:00:00Z"), DateTimeKind.Utc))
                return false;

            proveedor.DeletedAt = DateTime.UtcNow;
            _context.Proveedores.Update(proveedor);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
