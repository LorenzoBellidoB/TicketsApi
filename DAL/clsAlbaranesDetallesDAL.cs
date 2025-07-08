using DTO;
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

        public async Task<bool> InsertarUnidadesEnAlbaran(int idAlbaran, UnidadesDTO unidadesDto)
        {
            bool res = false;

            try
            {
                var albaran = await _context.Albaranes.FindAsync(idAlbaran);
                if (albaran == null)
                    return false;

                foreach (var unidadDto in unidadesDto.Unidades)
                {
                    var nuevaUnidad = new clsProductoUnidad
                    {
                        Peso = unidadDto.Peso,
                        PrecioKilo = unidadDto.PrecioKilo,
                        Etiqueta = unidadDto.Etiqueta,
                        FechaEntrada = DateTime.SpecifyKind(unidadDto.FechaEntrada, DateTimeKind.Utc),
                        Disponible = false,
                        IdProducto = unidadDto.IdProducto
                    };

                    _context.ProductosUnidades.Add(nuevaUnidad);
                    await _context.SaveChangesAsync(); // Guardar para obtener IdProductoUnidad generado

                    // Asociar unidad al albarán
                    var detalle = new clsAlbaranDetalle
                    {
                        IdAlbaran = idAlbaran,
                        IdProductoUnidad = nuevaUnidad.IdProductoUnidad
                    };

                    _context.AlbaranesDetalles.Add(detalle);
                }

                await _context.SaveChangesAsync();
                res = true;
            }
            catch (Exception ex)
            {
                // Log error
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
