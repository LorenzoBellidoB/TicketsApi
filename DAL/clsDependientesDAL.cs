using ENT;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class clsDependientesDAL
    {
        private readonly AppDbContext _context;

        public clsDependientesDAL(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<clsDependiente>> ObtenerDependientes()
        {
            return await _context.Dependientes
                .Include(d => d.Empresa) 
                .ToListAsync();
        }

        public async Task<clsDependiente> ObtenerDependientePorId(int id)
        {
            return await _context.Dependientes
                .Include(d => d.Empresa) 
                .FirstOrDefaultAsync(d => d.IdDependiente == id);
        }


        public async Task<bool> InsertarDependiente(clsDependiente dependiente)
        {
            bool res = false;
            _context.Dependientes.Add(dependiente);
            res = await _context.SaveChangesAsync() > 0;
            return res;
        }

        public async Task<bool> ActualizarDependiente(clsDependiente dependiente)
        {
            bool res = false;
            _context.Dependientes.Update(dependiente);
            res = await _context.SaveChangesAsync() > 0;
            return res;
        }

        public async Task<bool> EliminarDependiente(int id)
        {
            bool res = false;
            var dependiente = await _context.Dependientes.FindAsync(id);
            if (dependiente != null)
            {
                _context.Dependientes.Remove(dependiente);
                res = await _context.SaveChangesAsync() > 0;
            }
            return res;
        }
    }
}
