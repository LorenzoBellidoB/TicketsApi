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

    public async Task<List<clsTicket>> ObtenerTickets()
    {
        return await _context.Tickets
            .Include(t => t.Cliente)
            .Include(t => t.Dependiente)
            .Include(t => t.Empresa)
            .Include(t => t.Albaran)
            .ToListAsync();
    }

    public async Task<clsTicket> ObtenerTicketPorId(int id)
    {
        return await _context.Tickets
            .Include(t => t.Cliente)
            .Include(t => t.Dependiente)
            .Include(t => t.Empresa)
            .Include(t => t.Albaran)
            .FirstOrDefaultAsync(t => t.IdTicket == id);
    }

    public async Task<clsTicket> ObtenerTicketCompletoPorId(int id)
    {
        return await _context.Tickets
            .Include(t => t.Empresa)
            .Include(t => t.Cliente)
            .Include(t => t.Dependiente)
            .Include(t => t.Albaran)
                .ThenInclude(a => a.Empresa)
            .Include(t => t.Albaran)
                .ThenInclude(a => a.Cliente)
            .Include(t => t.Albaran)
                .ThenInclude(a => a.Dependiente)
            .Include(t => t.Albaran)
                .ThenInclude(a => a.Tickets)
            .Include(t => t.Albaran)
                .ThenInclude(a => a.Detalles)
                    .ThenInclude(d => d.ProductoUnidad)
                        .ThenInclude(pu => pu.Producto)
            .Include(t => t.Detalles)
                .ThenInclude(d => d.ProductoUnidad)
                    .ThenInclude(pu => pu.Producto)
            .FirstOrDefaultAsync(t => t.IdTicket == id);
    }


    public async Task<List<clsTicket>> ObtenerTicketsPorIdEmpresa(int idEmpresa)
    {
        return await _context.Tickets
            .Include(t => t.Empresa)
            .Where(t => t.Empresa.IdEmpresa == idEmpresa)
            .ToListAsync();
    }

    public async Task<bool> InsertarTicket(clsTicket ticket)
    {
        bool res = false;
        _context.Tickets.Add(ticket);
        res = await _context.SaveChangesAsync() > 0;
        return res;
    }

    public async Task<bool> ActualizarTicket(clsTicket ticket)
    {
        bool res = false;
        _context.Tickets.Update(ticket);
        res = await _context.SaveChangesAsync() > 0;
        return res;
    }

    public async Task<bool> EliminarTicket(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket == null || ticket.DeletedAt != DateTime.Parse("1111-01-01T00:00:00Z"))
            return false;

        ticket.DeletedAt = DateTime.UtcNow;
        _context.Tickets.Update(ticket);

        return await _context.SaveChangesAsync() > 0;
    }
}
