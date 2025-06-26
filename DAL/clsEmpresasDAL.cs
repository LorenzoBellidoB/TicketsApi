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
    }
}
