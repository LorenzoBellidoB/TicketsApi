using ENT;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class clsClientesDAL
    {
        private readonly AppDbContext _context;

        public clsClientesDAL(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<clsCliente>> ObtenerClientes()
        {
            return await _context.Clientes.Include(c => c.Empresa).ToListAsync();
        }

        public async Task<clsCliente> ObtenerClientePorId(int id)
        {
            return await _context.Clientes.Include(c => c.Empresa)
                .FirstOrDefaultAsync(c => c.IdCliente == id);
        }

        public async Task<List<clsCliente>> ObtenerClientesPorIdEmpresa(int idEmpresa)
        {
            return await _context.Clientes
                .Include(c => c.Empresa)
                .Where(c => c.Empresa.IdEmpresa == idEmpresa)
                .ToListAsync();
        }

        public async Task<int> InsertarCliente(clsCliente cliente)
        {
            int res = 0;
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            res = cliente.IdCliente;
            return res;
        }

        public async Task<bool> ActualizarCliente(clsCliente cliente)
        {
            bool res = false;
            _context.Clientes.Update(cliente);
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
