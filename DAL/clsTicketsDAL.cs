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
            .Include(t => t.Detalles)
                .ThenInclude(d => d.ProductoUnidad)
                    .ThenInclude(pu => pu.Producto)
            .FirstOrDefaultAsync(t => t.IdTicket == id);
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
        bool res = false;
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket != null)
        {
            _context.Tickets.Remove(ticket);
            res = await _context.SaveChangesAsync() > 0;
        }
        return res;
    }
}
