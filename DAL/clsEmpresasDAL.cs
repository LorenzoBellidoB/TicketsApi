using ENT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class clsEmpresasDAL
    {
        private readonly AppDbContext _context;

        public clsEmpresasDAL(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<clsEmpresa>> obtenerEmpresas()
        {
            return await _context.Empresas.ToListAsync();
        }

        public async Task<clsEmpresa> obtenerEmpresaPorId(int id)
        {
            return await _context.Empresas
                .FirstOrDefaultAsync(e => e.IdEmpresa == id);
        }

        public async Task<bool> insertarEmpresa(clsEmpresa empresa)
        {
            bool res = false;
            _context.Empresas.Add(empresa);
            res = await _context.SaveChangesAsync() > 0;
            return res;
        }

        public async Task<bool> actualizarEmpresa(clsEmpresa empresa)
        {
            bool res = false;
            _context.Empresas.Update(empresa);
            res = await _context.SaveChangesAsync() > 0;
            return res;
        }

        public async Task<bool> eliminarEmpresa(int id)
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
