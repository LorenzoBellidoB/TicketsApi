using ENT;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class clsAlbaranesDetallesDAL
    {
        private readonly AppDbContext _context;

        public clsAlbaranesDetallesDAL(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<clsAlbaranDetalle>> ObtenerAlbaranesDetalles()
        {
            return await _context.AlbaranesDetalles
                                 .Include(a => a.Albaran)
                                 .Include(a => a.ProductoUnidad)
                                 .ToListAsync();
        }

        public async Task<clsAlbaranDetalle> ObtenerAlbaranDetallePorId(int id)
        {
            return await _context.AlbaranesDetalles
                                 .Include(a => a.Albaran)
                                 .Include(a => a.ProductoUnidad)
                                 .FirstOrDefaultAsync(a => a.IdAlbaranDetalle == id);
        }

        public async Task<bool> InsertarAlbaranDetalle(clsAlbaranDetalle albaranDetalle)
        {
            bool res = false;
            _context.AlbaranesDetalles.Add(albaranDetalle);
            res = await _context.SaveChangesAsync() > 0;
            return res;
        }

        public async Task<bool> InsertarUnidadesEnAlbaran(int idAlbaran, List<int> unidades)
        {
            bool res = false;
            try
            {
                var albaran = await _context.Albaranes.FindAsync(idAlbaran);

                if (albaran == null)
                {
                    res = false;
                }

                foreach (var unidadId in unidades)
                {
                    var existe = await _context.AlbaranesDetalles
                        .AnyAsync(a => a.IdAlbaran == idAlbaran && a.IdProductoUnidad == unidadId);
                    if (existe) continue;

                    _context.AlbaranesDetalles.Add(new clsAlbaranDetalle
                    {
                        IdAlbaran = idAlbaran,
                        IdProductoUnidad = unidadId
                    });

                    var unidad = await _context.ProductosUnidades.FindAsync(unidadId);
                    if (unidad != null)
                    {
                        unidad.Disponible = false;
                    }
                }

                await _context.SaveChangesAsync();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }


        public async Task<bool> ActualizarAlbaranDetalle(clsAlbaranDetalle albaranDetalle)
        {
            bool res = false;
            _context.AlbaranesDetalles.Update(albaranDetalle);
            res = await _context.SaveChangesAsync() > 0;
            return res;
        }

        public async Task<bool> EliminarAlbaranDetalle(int id)
        {
            bool res = false;
            var albaranDetalle = await _context.AlbaranesDetalles.FindAsync(id);
            if (albaranDetalle != null)
            {
                _context.AlbaranesDetalles.Remove(albaranDetalle);
                res = await _context.SaveChangesAsync() > 0;
            }
            return res;
        }
    }
}
