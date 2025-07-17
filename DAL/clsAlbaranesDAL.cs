using DAL;
using ENT;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class clsAlbaranesDAL
{
    private readonly AppDbContext _context;

    public clsAlbaranesDAL(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<clsAlbaran>> ObtenerAlbaranes()
    {
        return await _context.Albaranes
            .ToListAsync();
    }

    public async Task<clsAlbaran> ObtenerAlbaranPorId(int id)
    {
        return await _context.Albaranes
            .Include(a => a.Cliente)
            .Include(a => a.Empresa)
            .Include(a => a.Dependiente)
            .FirstOrDefaultAsync(a => a.IdAlbaran == id);
    }

    public async Task<List<clsAlbaran>> ObtenerAlbaranesPorIdEmpresa(int idEmpresa)
    {
        return await _context.Albaranes
            .Include(a => a.Empresa)
            .Where(a => a.Empresa.IdEmpresa == idEmpresa)
            .ToListAsync();
    }

    public async Task<clsAlbaran> ObtenerAlbaranCompletoPorId(int id)
    {
        return await _context.Albaranes
            .Include(a => a.Empresa)
            .Include(a => a.Cliente)
            .Include(a => a.Dependiente)
            .Include(a => a.Detalles)
                .ThenInclude(d => d.ProductoUnidad)
                    .ThenInclude(pu => pu.Producto)
            .FirstOrDefaultAsync(a => a.IdAlbaran == id);
    }

    public async Task<bool> FacturarAsync(int id)
    {
        var albaran = await _context.Albaranes.FindAsync(id);
        if (albaran == null || albaran.Facturado)
            return false;

        albaran.Facturado = true;
        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<int> InsertarAlbaran(clsAlbaran albaran)
    {
        _context.Albaranes.Add(albaran);
        await _context.SaveChangesAsync();  
        return albaran.IdAlbaran;            
    }

    public async Task<bool> ActualizarAlbaran(clsAlbaran albaran)
    {
        _context.Albaranes.Update(albaran);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> EliminarAlbaran(int id)
    {
        var albaran = await _context.Albaranes.FindAsync(id);
        if (albaran == null || albaran.DeletedAt != DateTime.Parse("1111-01-01T00:00:00Z"))
            return false;

        var utcNow = DateTime.UtcNow;

        // Marcar el albarán como eliminado
        albaran.DeletedAt = utcNow;

        // Obtener y marcar como eliminados los detalles del albarán
        var albaranDetalles = await _context.AlbaranesDetalles
            .Where(da => da.IdAlbaran == id && da.DeletedAt == DateTime.Parse("1111-01-01T00:00:00Z"))
            .ToListAsync();

        foreach (var detalle in albaranDetalles)
            detalle.DeletedAt = utcNow;

        // Obtener y marcar como eliminados los tickets asociados
        var tickets = await _context.Tickets
            .Where(t => t.IdAlbaran == id && t.DeletedAt == DateTime.Parse("1111-01-01T00:00:00Z"))
            .ToListAsync();

        foreach (var ticket in tickets)
            ticket.DeletedAt = utcNow;

        // Obtener y marcar como eliminados los detalles de los tickets
        var ticketIds = tickets.Select(t => t.IdTicket).ToList();

        var ticketsDetalles = await _context.DetallesTicket
            .Where(td => ticketIds.Contains(td.IdTicket) && td.DeletedAt == DateTime.Parse("1111-01-01T00:00:00Z"))
            .ToListAsync();

        foreach (var detalle in ticketsDetalles)
            detalle.DeletedAt = utcNow;

        // Guardar cambios
        return await _context.SaveChangesAsync() > 0;
    }


}
