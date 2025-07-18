using ENT;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class clsEmpresasDAL
    {
        private readonly AppDbContext _context;

        public clsEmpresasDAL(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<clsEmpresa>> ObtenerEmpresas()
        {
            return await _context.Empresas.ToListAsync();
        }

        public async Task<clsEmpresa> ObtenerEmpresaPorId(int id)
        {
            return await _context.Empresas
                .FirstOrDefaultAsync(e => e.IdEmpresa == id);
        }

        public async Task<int> InsertarEmpresa(clsEmpresa empresa)
        {
            int res = 0;
            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();
            res = empresa.IdEmpresa;
            return res;
        }

        public async Task<bool> ActualizarEmpresa(clsEmpresa empresa)
        {
            bool res = false;
            _context.Empresas.Update(empresa);
            res = await _context.SaveChangesAsync() > 0;
            return res;
        }

        public async Task<bool> EliminarEmpresa(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);

            if (empresa == null || empresa.DeletedAt != DateTime.SpecifyKind(DateTime.Parse("1111-01-01T00:00:00Z"), DateTimeKind.Utc))
                return false;

            empresa.DeletedAt = DateTime.UtcNow;
            _context.Empresas.Update(empresa);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
