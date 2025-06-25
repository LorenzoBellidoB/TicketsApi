using DAL;
using ENT;
using Microsoft.EntityFrameworkCore;

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
            .Include(a => a.Cliente)
            .Include(a => a.Empresa)
            .Include(a => a.Dependiente)
            .ToListAsync();
    }
}
