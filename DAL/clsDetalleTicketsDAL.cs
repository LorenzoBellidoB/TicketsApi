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

    public async Task<List<clsDetalleTicket>> ObtenerDetallesTickets()
    {
        return await _context.DetallesTicket.ToListAsync();
    }

    public async Task<List<clsDetalleTicket>> obtenerDetallesPorTicketId(int ticketId)
    {
        return await _context.DetallesTicket
            .Where(d => d.IdTicket == ticketId)
            .Include(d => d.ProductoUnidad)
            .ToListAsync();
    }

    public async Task<bool> InsertarDetalleTicket(clsDetalleTicket dTicket)
    {
        bool res = false;
        _context.DetallesTicket.Add(dTicket);
        res = await _context.SaveChangesAsync() > 0;
        return res;
    }

    public async Task<bool> ActualizarDetalleTiquet(clsDetalleTicket dTicket)
    {
        bool res = false;
        _context.DetallesTicket.Update(dTicket);
        res = await _context.SaveChangesAsync() > 0;
        return res;
    }

    public async Task<bool> EliminarDetalleTicket(int id)
    {
        bool res = false;
        var dTicket = await _context.DetallesTicket.FindAsync(id);
        if (dTicket != null)
        {
            _context.DetallesTicket.Remove(dTicket);
            res = await _context.SaveChangesAsync() > 0;
        }
        return res;
    }

}
