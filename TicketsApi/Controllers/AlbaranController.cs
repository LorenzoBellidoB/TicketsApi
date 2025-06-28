using ENT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        //[SwaggerOperation(
        //    Summary = "Obtiene un listado con todos los albaranes",
        //    Description = "Este método obtiene todos los albaranes y los devuelve como un listado.<br>" +
        //    "Si no se encuentra ningún albaran devuelve un mensaje de error."
        //)]
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
        //[SwaggerOperation(
        //    Summary = "Obtiene un albaran según su id",
        //    Description = "Este método obtiene el albaran que coincida con el id proporcionado.<br>" +
        //    "Si no se encuentra ningún albaran devuelve un mensaje de error."
        //)]
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

        [HttpPost]
        //[SwaggerOperation(
        //    Summary = "Crea un nuevo albaran",
        //    Description = "Este método crea un nuevo albaran con los datos proporcionados en el cuerpo de la solicitud.<br>" +
        //    "Si se crea correctamente, devuelve un mensaje de éxito, de lo contrario, un mensaje de error."
        //)]
        public async Task<IActionResult> CrearAlbaran([FromBody] clsAlbaran albaran)
        {
            IActionResult salida;
            try
            {
                var resultado = await _albaranDAL.InsertarAlbaran(albaran);
                if (resultado)
                    salida = Ok("Albaran creada correctamente");
                else
                    salida = BadRequest("No se pudo crear el albaran");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpPut("{id}")]
        //[SwaggerOperation(
        //    Summary = "Actualiza un albaran",
        //    Description = "Este método actualiza un albaran con los datos proporcionados en el cuerpo de la solicitud.<br>" +
        //    "Si se actualiza correctamente, devuelve un mensaje de éxito, de lo contrario, un mensaje de error."
        //    )]
        public async Task<IActionResult> ActualizarAlbaran(int id, [FromBody] clsAlbaran albaran)
        {
            IActionResult salida;
            try
            {
                if (id != albaran.IdAlbaran)
                    salida = BadRequest("El ID de la URL no coincide con el del objeto");

                var albaranExistente = await _albaranDAL.ObtenerAlbaranPorId(id);
                if (albaranExistente == null)
                    salida = NotFound("Albaran no encontrado");

                var resultado = await _albaranDAL.ActualizarAlbaran(albaran);
                salida = resultado ? Ok("Albaran actualizado correctamente") : BadRequest("No se pudo actualizar el albaran");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpDelete("{id}")]
        //[SwaggerOperation(
        //    Summary = "Elimina un albaran",
        //    Description = "Este método elimina un albaran según su id.<br>" +
        //    "Si se elimina correctamente, devuelve un mensaje de éxito, de lo contrario, un mensaje de error."
        //)]
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
