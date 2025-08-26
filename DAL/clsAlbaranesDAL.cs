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

    public async Task<List<clsAlbaran>> ObtenerAlbaranesPorIdEmpresaFacturados(int idEmpresa)
    {
        return await _context.Albaranes.Include(a => a.Cliente)
            .Include(a => a.Empresa)
            .Include(a => a.Dependiente)
            .Where(a => a.Facturado && a.IdEmpresa == idEmpresa)
            .ToListAsync();
    }

    public async Task<List<clsAlbaran>> ObtenerAlbaranesPorIdEmpresaNoFacturados(int idEmpresa)
    {
        return await _context.Albaranes.Include(a => a.Cliente)
            .Include(a => a.Empresa)
            .Include(a => a.Dependiente)
            .Where(a => !a.Facturado && a.IdEmpresa == idEmpresa)
            .ToListAsync();
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
        .Include(a => a.Detalles)
            .ThenInclude(d => d.Servicio) // 👈 necesario para cargar Servicio
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
        var albaranOriginal = await _context.Albaranes
            .FirstOrDefaultAsync(a => a.IdAlbaran == albaran.IdAlbaran);

        if (albaranOriginal == null)
            return false;

        albaranOriginal.Serie = albaran.Serie;
        albaranOriginal.Fecha = albaran.Fecha;
        albaranOriginal.Importe = albaran.Importe;
        albaranOriginal.Descuento = albaran.Descuento;
        albaranOriginal.DescuentoPPago = albaran.DescuentoPPago;
        albaranOriginal.Descripcion = albaran.Descripcion;
        albaranOriginal.Facturado = albaran.Facturado;
        albaranOriginal.IdCliente = albaran.IdCliente;
        albaranOriginal.Kilos = albaran.Kilos;
        albaranOriginal.IdEmpresa = albaran.IdEmpresa;
        albaranOriginal.IdDependiente = albaran.IdDependiente;

        return await _context.SaveChangesAsync() > 0;
    }


    public async Task<bool> EliminarAlbaran(int id)
    {
        var albaran = await _context.Albaranes.FindAsync(id);
        if (albaran == null || albaran.DeletedAt != DateTime.SpecifyKind(DateTime.Parse("1111-01-01T00:00:00Z"), DateTimeKind.Utc))
            return false;

        var utcNow = DateTime.UtcNow;

        // Marcar el albarán como eliminado
        albaran.DeletedAt = utcNow;

        // Obtener y marcar como eliminados los detalles del albarán
        var albaranDetalles = await _context.AlbaranesDetalles
            .Where(da => da.IdAlbaran == id && da.DeletedAt == DateTime.SpecifyKind(DateTime.Parse("1111-01-01T00:00:00Z"), DateTimeKind.Utc))
            .ToListAsync();

        foreach (var detalle in albaranDetalles)
            detalle.DeletedAt = utcNow;

        // Obtener y marcar como eliminados los tickets asociados
        var tickets = await _context.Tickets
            .Where(t => t.IdAlbaran == id && t.DeletedAt == DateTime.SpecifyKind(DateTime.Parse("1111-01-01T00:00:00Z"), DateTimeKind.Utc))
            .ToListAsync();

        foreach (var ticket in tickets)
            ticket.DeletedAt = utcNow;

        // Obtener y marcar como eliminados los detalles de los tickets
        var ticketIds = tickets.Select(t => t.IdTicket).ToList();

        var ticketsDetalles = await _context.DetallesTicket
            .Where(td => ticketIds.Contains(td.IdTicket) && td.DeletedAt == DateTime.SpecifyKind(DateTime.Parse("1111-01-01T00:00:00Z"), DateTimeKind.Utc))
            .ToListAsync();

        foreach (var detalle in ticketsDetalles)
            detalle.DeletedAt = utcNow;

        // Guardar cambios
        return await _context.SaveChangesAsync() > 0;
    }


}
