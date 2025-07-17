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

        public async Task<List<clsDependiente>> ObtenerDependientesPorIdEmpresa(int idEmpresa)
        {
            return await _context.Dependientes
                .Include(d => d.Empresa)
                .Where(d => d.Empresa.IdEmpresa == idEmpresa)
                .ToListAsync();
        }


        public async Task<int> InsertarDependiente(clsDependiente dependiente)
        {
            int res = 0;
            _context.Dependientes.Add(dependiente);
            await _context.SaveChangesAsync();
            res = dependiente.IdDependiente;
            return res;
        }

        public async Task<bool> ActualizarDependiente(clsDependiente dependiente)
        {
            bool res = false;
            _context.Dependientes.Update(dependiente);
            res = await _context.SaveChangesAsync() > 0;
            return res;
        }

        public async Task<bool> EliminarCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null || cliente.DeletedAt != DateTime.Parse("1111-01-01T00:00:00Z"))
                return false;

            cliente.DeletedAt = DateTime.UtcNow;
            _context.Clientes.Update(cliente);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}


