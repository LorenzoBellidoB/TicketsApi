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
                                 .Include(a => a.Servicio)
                                 .ToListAsync();
        }

        public async Task<clsAlbaranDetalle> ObtenerAlbaranDetallePorId(int id)
        {
            return await _context.AlbaranesDetalles
                                 .Include(a => a.Albaran)
                                 .Include(a => a.ProductoUnidad)
                                 .Include(a => a.Servicio)
                                 .FirstOrDefaultAsync(a => a.IdAlbaranDetalle == id);
        }

        public async Task<bool> InsertarAlbaranDetalle(clsAlbaranDetalle albaranDetalle)
        {
            bool res = false;
            _context.AlbaranesDetalles.Add(albaranDetalle);
            res = await _context.SaveChangesAsync() > 0;
            return res;
        }

        public async Task<bool> InsertarDetallesEnAlbaran(int idAlbaran, AlbaranDetalleRequestDTO dto)
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    var albaran = await _context.Albaranes.FindAsync(idAlbaran);
                    if (albaran == null)
                        throw new Exception($"No se encontró el albarán con ID {idAlbaran}");

                    // Insertar unidades
                    foreach (var unidadDto in dto.Unidades)
                    {
                        var unidadExistente = await _context.ProductosUnidades
                            .FirstOrDefaultAsync(u => u.IdProductoUnidad == unidadDto.IdProductoUnidad);

                        if (unidadExistente == null)
                            throw new Exception($"No se encontró la unidad con ID {unidadDto.IdProductoUnidad}");

                        unidadExistente.Disponible = false;

                        var detalle = new clsAlbaranDetalle
                        {
                            IdAlbaran = idAlbaran,
                            IdProductoUnidad = unidadExistente.IdProductoUnidad,
                            IdServicio = dto.Servicios?.FirstOrDefault()?.IdServicio // 👈 aquí lo enganchas
                        };

                        _context.AlbaranesDetalles.Add(detalle);
                    }

                    await _context.SaveChangesAsync();
                    await _context.Database.ExecuteSqlRawAsync($"SELECT actualizar_totales_albaran({idAlbaran})");

                    await transaction.CommitAsync();
                    return true;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }



        //public async Task<bool> InsertarUnidadesEnAlbaran(int idAlbaran, UnidadesDTO unidadesDto)
        //{
        //    try
        //    {
        //        var albaran = await _context.Albaranes.FindAsync(idAlbaran);
        //        if (albaran == null)
        //            throw new Exception($"No se encontró el albarán con ID {idAlbaran}");

        //        foreach (var unidadDto in unidadesDto.Unidades)
        //        {
        //            // Buscar la unidad ya existente
        //            var unidadExistente = await _context.ProductosUnidades
        //                .FirstOrDefaultAsync(u => u.IdProductoUnidad == unidadDto.IdProductoUnidad);

        //            if (unidadExistente == null)
        //                throw new Exception($"No se encontró la unidad con ID {unidadDto.IdProductoUnidad}");

        //            // Marcar la unidad como no disponible en la entidad
        //            unidadExistente.Disponible = false;

        //            // Crear el detalle usando la unidad existente
        //            var detalle = new clsAlbaranDetalle
        //            {
        //                IdAlbaran = idAlbaran,
        //                IdProductoUnidad = unidadExistente.IdProductoUnidad
        //            };

        //            _context.AlbaranesDetalles.Add(detalle);
        //        }

        //        await _context.SaveChangesAsync();

        //        await _context.Database.ExecuteSqlRawAsync($"SELECT actualizar_totales_albaran({idAlbaran})");

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        var innerMessage = ex.InnerException?.Message ?? "No hay inner exception";
        //        throw new Exception($"Error al insertar unidades en el albarán {idAlbaran}: {ex.Message} - Inner: {innerMessage}", ex);
        //    }
        //}

        public async Task<bool> ActualizarAlbaranDetalle(clsAlbaranDetalle detalle)
        {
            var original = await _context.AlbaranesDetalles
                .FirstOrDefaultAsync(d => d.IdAlbaranDetalle == detalle.IdAlbaranDetalle);

            if (original == null) return false;

            original.IdAlbaran = detalle.IdAlbaran;
            original.IdProductoUnidad = detalle.IdProductoUnidad;
            original.IdServicio = detalle.IdServicio;

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
