using DAL;
using DTO;
using ENT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace TicketsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioController : Controller
    {
        private readonly clsServiciosDAL _servicioDAL;

        public ServicioController(clsServiciosDAL servicioDAL)
        {
            _servicioDAL = servicioDAL;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene un listado con todos los servicios",
            Description = "Este método obtiene todos los servicios y los devuelve como un listado.<br>" +
            "Si no se encuentra ningún servicio devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetServicios()
        {
            IActionResult salida;
            try
            {
                var servicios = await _servicioDAL.ObtenerServicios();
                if (servicios.Count == 0)
                {
                    salida = NotFound("No se han encontrado servicios");
                }
                else
                {
                    salida = Ok(servicios);
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
            Summary = "Obtiene un servicio según su id",
            Description = "Este método obtiene el servicio que coincida con el id proporcionado.<br>" +
            "Si no se encuentra ningún servicio devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetServicio(int id)
        {
            IActionResult salida;
            try
            {
                var servicio = await _servicioDAL.ObtenerServicioPorId(id);
                if (servicio == null)
                {
                    salida = NotFound("No se ha encontrado un servicio con ese id");
                }
                else
                {
                    salida = Ok(servicio);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }

            return salida;
        }

        [HttpGet("empresa/{idEmpresa}")]
        [SwaggerOperation(
            Summary = "Obtiene un servicio según su empresa",
            Description = "Este método obtiene los servicios que coincida con el id de la empresa proporcionado.<br>" +
            "Si no se encuentra ningún servicio devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetServiciosPorEmpresa(int idEmpresa)
        {
            IActionResult salida;
            try
            {
                var servicios = await _servicioDAL.ObtenerServiciosPorIdEmpresa(idEmpresa);
                if (servicios == null)
                {
                    salida = NotFound("No se ha encontrado un servicio con ese id de empresa");
                }
                else
                {
                    salida = Ok(servicios);
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
            Summary = "Crea un nuevo servicio",
            Description = "Este método crea un nuevo servicio en la base de datos.<br>" +
            "Si la creación es exitosa, devuelve un mensaje de éxito."
        )]
        public async Task<IActionResult> CrearProducto([FromBody] ServicioDTO dto)
        {
            IActionResult salida;
            try
            {
                clsServicio servicio = new clsServicio()
                {
                    Nombre = dto.Nombre,
                    Precio = dto.Precio,
                    IdEmpresa = dto.IdEmpresa
                };
                var resultado = await _servicioDAL.InsertarServicio(servicio);
                if (resultado > 0)
                    salida = Ok(resultado);
                else
                    salida = BadRequest(resultado);
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Actualiza un servicio existente",
            Description = "Este método actualiza un servicio existente con los datos proporcionados.<br>" +
            "Si la actualización es exitosa, devuelve un mensaje de éxito."
        )]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] ServicioDTO dto)
        {
            IActionResult salida;
            try
            {
                clsServicio servicio = new clsServicio()
                {
                    IdServicio = id,
                    Nombre = dto.Nombre,
                    Precio = dto.Precio,
                    IdEmpresa = dto.IdEmpresa
                };
                if (id != servicio.IdServicio)
                    salida = BadRequest("El ID de la URL no coincide con el del objeto");

                var servicioExistente = await _servicioDAL.ObtenerServicioPorId(id);
                if (servicioExistente == null)
                    salida = NotFound("Producto no encontrado");

                var resultado = await _servicioDAL.ActualizarServicio(servicio);
                salida = resultado ? Ok(resultado) : BadRequest("No se pudo actualizar el servicio");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Elimina un servicio existente",
            Description = "Este método elimina un servicio existente según su id.<br>" +
            "Si la eliminación es exitosa, devuelve un mensaje de éxito."
        )]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            IActionResult salida;
            try
            {
                var resultado = await _servicioDAL.EliminarServicio(id);
                salida = resultado ? Ok("Producto eliminado correctamente") : NotFound("No se encontró el servicio para eliminar");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }
    }
}
