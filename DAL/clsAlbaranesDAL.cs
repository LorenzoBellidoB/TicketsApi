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
            .Include(a => a.Cliente)
            .Include(a => a.Dependiente)
            .Include(a => a.Detalles)
                .ThenInclude(d => d.ProductoUnidad)
                    .ThenInclude(pu => pu.Producto)
                    .ThenInclude(p => p.Proveedor)
            .FirstOrDefaultAsync(a => a.IdAlbaran == id);
    }


    public async Task<bool> InsertarAlbaran(clsAlbaran albaran)
    {
        _context.Albaranes.Add(albaran);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> ActualizarAlbaran(clsAlbaran albaran)
    {
        _context.Albaranes.Update(albaran);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> EliminarAlbaran(int id)
    {
        var albaran = await _context.Albaranes.FindAsync(id);
        if (albaran != null)
        {
            _context.Albaranes.Remove(albaran);
            return await _context.SaveChangesAsync() > 0;
        }
        return false;
    }
}
