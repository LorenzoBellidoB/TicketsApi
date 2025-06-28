using DAL;
using ENT;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class clsTicketsDAL
{
    private readonly AppDbContext _context;

    public clsTicketsDAL(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<clsTicket>> obtenerTickets()
    {
        return await _context.Tickets
            .Include(t => t.Cliente)
            .Include(t => t.Dependiente)
            .Include(t => t.Empresa)
            .Include(t => t.Albaran)
            .ToListAsync();
    }

    public async Task<clsTicket> obtenerTicketPorId(int id)
    {
        return await _context.Tickets
            .Include(t => t.Cliente)
            .Include(t => t.Dependiente)
            .Include(t => t.Empresa)
            .Include(t => t.Albaran)
            .Include(t => t.Detalles)
                .ThenInclude(d => d.ProductoUnidad)
                    .ThenInclude(pu => pu.Producto)
            .FirstOrDefaultAsync(t => t.IdTicket == id);
    }
}
