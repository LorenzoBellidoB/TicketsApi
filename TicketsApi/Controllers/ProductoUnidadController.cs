using DAL;
using DTO;
using ENT;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace TicketsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoUnidadController : Controller
    {
        private readonly clsProductosUnidadesDAL _productosUnidadesDAL;

        public ProductoUnidadController(clsProductosUnidadesDAL productoUnidadDAL)
        {
            _productosUnidadesDAL = productoUnidadDAL;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene un listado con todos las unidades de productos",
            Description = "Este método obtiene todos las unidades de productos y los devuelve como un listado.<br>" +
            "Si no se encuentra ninguna unidad de producto devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetProductosUnidades()
        {
            IActionResult salida;
            try
            {
                var productosUnidades = await _productosUnidadesDAL.ObtenerProductoUnidades();
                if (productosUnidades.Count == 0)
                {
                    salida = NotFound("No se han encontrado productosUnidades");
                }
                else
                {
                    salida = Ok(productosUnidades);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }
            return salida;
        }

        [HttpGet("disponibles/{id}/empresa")]
        [SwaggerOperation(
           Summary = "Obtiene un listado con todos las unidades de productos disponibles",
           Description = "Este método obtiene todos las unidades de productos y los devuelve como un listado.<br>" +
           "Si no se encuentra ninguna unidad de producto devuelve un mensaje de error."
       )]
        public async Task<IActionResult> GetProductosUnidadesDisponibles(int id)
        {
            IActionResult salida;
            try
            {
                var productosUnidades = await _productosUnidadesDAL.ObtenerProductoUnidadesDisponibles(id);
                if (productosUnidades.Count == 0)
                {
                    salida = NotFound("No se han encontrado productosUnidades");
                }
                else
                {
                    salida = Ok(productosUnidades);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }
            return salida;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obtiene una unidad de producto según su id",
            Description = "Este método obtiene la unidad de producto que coincida con el id proporcionado.<br>" +
            "Si no se encuentra ninguna unidad de producto devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetProductoUnidad(int id)
        {
            IActionResult salida;
            try
            {
                var productoUnidad = await _productosUnidadesDAL.ObtenerProductoUnidadPorId(id);
                if (productoUnidad == null)
                {
                    salida = NotFound("No se ha encontrado un productoUnidad con ese id");
                }
                else
                {
                    salida = Ok(productoUnidad);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }

            return salida;
        }

        [HttpGet("producto/{id}")]
        [SwaggerOperation(
            Summary = "Obtiene las unidades de producto asociadas a un producto según su id",
            Description = "Este método obtiene todas las unidades de producto asociadas al producto que coincida con el id proporcionado.<br>" +
            "Si no se encuentra ninguna unidad de producto asociada al producto devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetProductoUnidadesPorProductoId(int id)
        {
            IActionResult salida;
            try
            {
                var productosUnidades = await _productosUnidadesDAL.ObtenerProductoUnidadesPorProductoId(id);
                if (productosUnidades == null)
                {
                    salida = NotFound("No se ha encontrado producto unidad asociado a ese producto con ese id");
                }
                else
                {
                    salida = Ok(productosUnidades);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }

            return salida;
        }

        [HttpGet("empresa/{id}")]
        [SwaggerOperation(
         Summary = "Obtiene las unidades de producto asociadas a una empresa según su id",
         Description = "Este método obtiene todas las unidades de producto asociadas a la empresa que coincida con el id proporcionado.<br>" +
         "Si no se encuentra ninguna unidad de producto asociada a la empresa devuelve un mensaje de error."
     )]
        public async Task<IActionResult> GetProductoUnidadesPorEmpresaId(int id)
        {
            IActionResult salida;
            try
            {
                var productosUnidades = await _productosUnidadesDAL.ObtenerProductoUnidadesPorEmpresaId(id);
                if (productosUnidades == null)
                {
                    salida = NotFound("No se ha encontrado producto unidad asociado a esa empresa con ese id");
                }
                else
                {
                    salida = Ok(productosUnidades);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }

            return salida;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Crea una nueva unidad de producto",
            Description = "Este método crea una nueva unidad de producto y la inserta en la base de datos.<br>" +
            "Si se crea correctamente devuelve un mensaje de éxito, si no, un mensaje de error."
        )]
        public async Task<IActionResult> CrearProductoUnidad([FromBody] ProductoUnidadDTO dto)
        {
            IActionResult salida;
            try
            {
                var productoUnidad = new clsProductoUnidad
                {
                    IdProducto = dto.IdProducto,
                    Peso = dto.Peso,
                    PrecioKilo = dto.PrecioKilo,
                    Etiqueta = dto.Etiqueta,
                    FechaEntrada = dto.FechaEntrada,
                    Disponible = dto.Disponible,
                    IdProductoUnidad = dto.IdProductoUnidad,
                    IdEmpresa = dto.IdEmpresa


                };
                var resultado = await _productosUnidadesDAL.InsertarProductoUnidad(productoUnidad);
                if (resultado != null)
                    salida = Ok(resultado);
                else
                    salida = BadRequest("No se pudo crear el productoUnidad");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpPatch("{id}/disponible")]
        public async Task<IActionResult> FacturarAlbaran(int id)
        {
            var success = await _productosUnidadesDAL.MarcarDisponible(id);
            if (!success)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Actualiza una unidad de producto",
            Description = "Este método actualiza una unidad de producto existente en la base de datos.<br>" +
            "Si se actualiza correctamente devuelve un mensaje de éxito, si no, un mensaje de error."
        )]
        public async Task<IActionResult> ActualizarProductoUnidad(int id, [FromBody] ProductoUnidadDTO dto)
        {
            IActionResult salida;
            try
            {
                var productoUnidad = new clsProductoUnidad
                {
                    IdProducto = dto.IdProducto,
                    Peso = dto.Peso,
                    PrecioKilo = dto.PrecioKilo,
                    Etiqueta = dto.Etiqueta,
                    FechaEntrada = dto.FechaEntrada,
                    Disponible = dto.Disponible,
                    IdProductoUnidad = dto.IdProductoUnidad,
                    IdEmpresa = dto.IdEmpresa


                };
                if (id != productoUnidad.IdProductoUnidad)
                    salida = BadRequest("El ID de la URL no coincide con el del objeto");

                var productoUnidadExistente = await _productosUnidadesDAL.ObtenerProductoUnidadPorId(id);
                if (productoUnidadExistente == null)
                    salida = NotFound("ProductoUnidad no encontrado");

                var resultado = await _productosUnidadesDAL.ActualizarProductoUnidad(productoUnidad);
                salida = resultado ? Ok("ProductoUnidad actualizado correctamente") : BadRequest("No se pudo actualizar el productoUnidad");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Elimina una unidad de producto",
            Description = "Este método elimina una unidad de producto existente en la base de datos.<br>" +
            "Si se elimina correctamente devuelve un mensaje de éxito, si no, un mensaje de error."
        )]
        public async Task<IActionResult> EliminarProductoUnidad(int id)
        {
            IActionResult salida;
            try
            {
                var resultado = await _productosUnidadesDAL.EliminarProductoUnidad(id);
                salida = resultado ? Ok("ProductoUnidad eliminado correctamente") : NotFound("No se encontró el productoUnidad para eliminar");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message +
                                    (e.InnerException != null ? " | Inner: " + e.InnerException.Message : ""));
            }

            return salida;
        }
    }
}
