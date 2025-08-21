using ENT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class clsServiciosDAL
    {
        private readonly AppDbContext _context;

        public clsServiciosDAL(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<clsServicio>> ObtenerServicios()
        {
            return await _context.Servicios.Include(s => s.Empresa).ToListAsync();
        }

        public async Task<clsServicio> ObtenerServicioPorId(int id)
        {
            return await _context.Servicios.Include(s => s.Empresa)
                .FirstOrDefaultAsync(s => s.IdServicio == id);
        }

        public async Task<List<clsServicio>> ObtenerServiciosPorIdEmpresa(int idEmpresa)
        {
            return await _context.Servicios
                .Include(s => s.Empresa)
                .Where(s => s.Empresa.IdEmpresa == idEmpresa)
                .ToListAsync();
        }

        public async Task<int> InsertarServicio(clsServicio servicio)
        {
            int res = 0;
            _context.Servicios.Add(servicio);
            await _context.SaveChangesAsync();
            res = servicio.IdServicio;
            return res;
        }

        public async Task<bool> ActualizarServicio(clsServicio servicio)
        {
            var servicioOriginal = await _context.Servicios
                .FirstOrDefaultAsync(s => s.IdServicio == servicio.IdServicio);

            if (servicioOriginal == null)
                return false;

            servicioOriginal.Nombre = servicio.Nombre;
            servicioOriginal.Precio = servicio.Precio;
            servicioOriginal.IdEmpresa = servicio.IdEmpresa;

            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> EliminarServicio(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);

            if (servicio == null || servicio.DeletedAt != DateTime.SpecifyKind(DateTime.Parse("1111-01-01T00:00:00Z"), DateTimeKind.Utc))
                return false;

            servicio.DeletedAt = DateTime.UtcNow;
            _context.Servicios.Update(servicio);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
