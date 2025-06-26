using DAL;
using ENT;
using Microsoft.EntityFrameworkCore;

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
            .Include(t => t.Detalles)
            .FirstOrDefaultAsync(t => t.IdTicket == id);
    }
}
