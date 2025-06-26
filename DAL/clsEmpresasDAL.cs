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

        public async Task<bool> InsertarEmpresa(clsEmpresa empresa)
        {
            bool res = false;
            _context.Empresas.Add(empresa);
            res = await _context.SaveChangesAsync() > 0;
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
            bool res = false;
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa != null)
            {
                _context.Empresas.Remove(empresa);
                res = await _context.SaveChangesAsync() > 0;
            }
            return res;
        }
    }
}
