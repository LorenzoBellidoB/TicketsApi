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
            try
            {
                var albaran = await _context.Albaranes.FindAsync(idAlbaran);
                if (albaran == null)
                    throw new Exception($"No se encontró el albarán con ID {idAlbaran}");

                foreach (var unidadDto in unidadesDto.Unidades)
                {
                    var nuevaUnidad = new clsProductoUnidad
                    {
                        Peso = unidadDto.Peso,
                        PrecioKilo = unidadDto.PrecioKilo,
                        Etiqueta = unidadDto.Etiqueta,
                        FechaEntrada = DateTime.SpecifyKind(unidadDto.FechaEntrada, DateTimeKind.Utc),
                        Disponible = unidadDto.Disponible,
                        IdProducto = unidadDto.IdProducto
                    };

                    _context.ProductosUnidades.Add(nuevaUnidad);
                    await _context.SaveChangesAsync(); // Genera el ID

                    var detalle = new clsAlbaranDetalle
                    {
                        IdAlbaran = idAlbaran,
                        IdProductoUnidad = nuevaUnidad.IdProductoUnidad
                    };

                    _context.AlbaranesDetalles.Add(detalle);
                }

                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync($"SELECT actualizar_totales_albaran({idAlbaran})");

                return true;
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? "No hay inner exception";
                throw new Exception($"Error al insertar unidades en el albarán {idAlbaran}: {ex.Message} - Inner: {innerMessage}", ex);
            }

        }




        public async Task<bool> ActualizarAlbaranDetalle(clsAlbaranDetalle detalle)
        {
            var original = await _context.AlbaranesDetalles
                .FirstOrDefaultAsync(d => d.IdAlbaranDetalle == detalle.IdAlbaranDetalle);

            if (original == null) return false;

            original.IdAlbaran = detalle.IdAlbaran;
            original.IdProductoUnidad = detalle.IdProductoUnidad;

            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> EliminarAlbaranDetalle(int id)
        {
            var albaranDetalle = await _context.AlbaranesDetalles.FindAsync(id);

            // Ya está eliminado o no existe
            if (albaranDetalle == null || albaranDetalle.DeletedAt != DateTime.SpecifyKind(DateTime.Parse("1111-01-01T00:00:00Z"), DateTimeKind.Utc))
                return false;

            albaranDetalle.DeletedAt = DateTime.UtcNow;
            _context.AlbaranesDetalles.Update(albaranDetalle);

            return await _context.SaveChangesAsync() > 0;
        }

    }
}
