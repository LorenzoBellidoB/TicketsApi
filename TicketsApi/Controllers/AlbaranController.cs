using ENT;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TicketsApi.DTO;

namespace TicketsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbaranController : Controller
    {
        private readonly clsAlbaranesDAL _albaranDAL;

        public AlbaranController(clsAlbaranesDAL albaranDAL)
        {
            _albaranDAL = albaranDAL;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene un listado con todos los albaranes",
            Description = "Este método obtiene todos los albaranes y los devuelve como un listado.<br>" +
            "Si no se encuentra ningún albaran devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetAlbaranes()
        {
            IActionResult salida;
            try
            {
                var albaranes = await _albaranDAL.ObtenerAlbaranes();
                if (albaranes.Count == 0)
                {
                    salida = NotFound("No se han encontrado albaranes");
                }
                else
                {
                    salida = Ok(albaranes);
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
            Summary = "Obtiene un albaran según su id",
            Description = "Este método obtiene el albaran que coincida con el id proporcionado.<br>" +
            "Si no se encuentra ningún albaran devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetAlbaran(int id)
        {
            IActionResult salida;
            try
            {
                var albaran = await _albaranDAL.ObtenerAlbaranPorId(id);
                if (albaran == null)
                {
                    salida = NotFound("No se ha encontrado un albaran con ese id");
                }
                else
                {
                    salida = Ok(albaran);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }

            return salida;
        }

        [HttpGet("{id}/detalles")]
        public async Task<ActionResult<clsAlbaran>> GetAlbaranDetalle(int id)
        {
            var albaran = await _albaranDAL.ObtenerAlbaranCompletoPorId(id);
            if (albaran == null)
                return NotFound();

            return Ok(albaran);
        }

        [HttpGet("empresa/{idEmpresa}")]
        [SwaggerOperation(
            Summary = "Obtiene un albaran según su empresa",
            Description = "Este método obtiene los albaranes que coincida con el id de la empresa proporcionado.<br>" +
            "Si no se encuentra ningún albaran devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetAlbaranesPorEmpresa(int idEmpresa)
        {
            IActionResult salida;
            try
            {
                var albaranes = await _albaranDAL.ObtenerAlbaranesPorIdEmpresa(idEmpresa);
                if (albaranes == null)
                {
                    salida = NotFound("No se ha encontrado un dependiente con ese id de empresa");
                }
                else
                {
                    salida = Ok(albaranes);
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
            Summary = "Crea un nuevo albaran",
            Description = "Este método crea un nuevo albaran con los datos proporcionados en el cuerpo de la solicitud.<br>" +
            "Si se crea correctamente, devuelve un mensaje de éxito, de lo contrario, un mensaje de error."
        )]
        public async Task<IActionResult> CrearAlbaran([FromBody] AlbaranDTO dto)
        {
            // Mapeo manual de DTO a entidad
            var albaran = new clsAlbaran
            {
                Serie = dto.Serie,
                Numero = dto.Numero,
                Fecha = dto.Fecha,
                Importe = dto.Importe,
                Descripcion = dto.Descripcion,
                Facturado = dto.Facturado,
                IdCliente = dto.IdCliente,
                Kilos = dto.Kilos,
                IdEmpresa = dto.IdEmpresa,
                IdDependiente = dto.IdDependiente
            };

            var success = await _albaranDAL.InsertarAlbaran(albaran);
            if (!success)
                return BadRequest("No se pudo crear el albarán");

            // Puedes devolver CreatedAtAction si quieres la URL del recurso creado
            return Ok("Albarán creada correctamente");
        }

        [HttpPatch("{id}/facturar")]
        public async Task<IActionResult> FacturarAlbaran(int id)
        {
            var success = await _albaranDAL.FacturarAsync(id);
            if (!success)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Actualiza un albaran",
            Description = "Este método actualiza un albaran con los datos proporcionados en el cuerpo de la solicitud.<br>" +
            "Si se actualiza correctamente, devuelve un mensaje de éxito, de lo contrario, un mensaje de error."
            )]
        public async Task<IActionResult> ActualizarAlbaran(int id, [FromBody] AlbaranDTO dto)
        {
            if (id != dto.IdAlbaran)
                return BadRequest("El ID de la URL no coincide con el del objeto");

            var existente = await _albaranDAL.ObtenerAlbaranPorId(id);
            if (existente == null)
                return NotFound("Albarán no encontrado");

            // Mapeo de campos editables
            existente.Serie = dto.Serie;
            existente.Numero = dto.Numero;
            existente.Fecha = dto.Fecha;
            existente.Importe = dto.Importe;
            existente.Descripcion = dto.Descripcion;
            existente.Facturado = dto.Facturado;
            existente.IdCliente = dto.IdCliente;
            existente.Kilos = dto.Kilos;
            existente.IdEmpresa = dto.IdEmpresa;
            existente.IdDependiente = dto.IdDependiente;

            var result = await _albaranDAL.ActualizarAlbaran(existente);
            if (!result)
                return BadRequest("No se pudo actualizar el albarán");

            return Ok("Albarán actualizado correctamente");
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Elimina un albaran",
            Description = "Este método elimina un albaran según su id.<br>" +
            "Si se elimina correctamente, devuelve un mensaje de éxito, de lo contrario, un mensaje de error."
        )]
        public async Task<IActionResult> EliminarAlbaran(int id)
        {
            IActionResult salida;
            try
            {
                var resultado = await _albaranDAL.EliminarAlbaran(id);
                salida = resultado ? Ok("Albaran eliminado correctamente") : NotFound("No se encontró el albaran para eliminar");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }
    }
}
