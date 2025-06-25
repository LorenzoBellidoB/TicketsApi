using DAL;
using ENT;
using Microsoft.EntityFrameworkCore;

public class clsDetalleTicketsDAL
{
    private readonly AppDbContext _context;

    public clsDetalleTicketsDAL(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<clsDetalleTicket>> ObtenerDetallesPorTicketId(int ticketId)
    {
        return await _context.DetallesTicket
            .Where(d => d.IdTicket == ticketId)
            .Include(d => d.Producto)
            .ToListAsync();
    }
}
