using ENT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
