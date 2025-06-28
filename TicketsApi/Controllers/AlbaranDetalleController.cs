using DAL;
using ENT;
using Microsoft.AspNetCore.Mvc;

namespace TicketsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbaranDetalleController : Controller
    {
        private readonly clsAlbaranesDetallesDAL _albaranesDetallesDAL;

        public AlbaranDetalleController(clsAlbaranesDetallesDAL albaranesDetallesDAL)
        {
            _albaranesDetallesDAL = albaranesDetallesDAL;
        }

        [HttpGet]
        public async Task<IActionResult> GetAlbaranesDetalles()
        {
            IActionResult salida;
            try
            {
                var albaranesDetalles = await _albaranesDetallesDAL.ObtenerAlbaranesDetalles();
                if (albaranesDetalles.Count == 0)
                {
                    salida = NotFound("No se han encontrado albaranesDetalles");
                }
                else
                {
                    salida = Ok(albaranesDetalles);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }
            return salida;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbaranDetalle(int id)
        {
            IActionResult salida;
            try
            {
                var albaranDetalle = await _albaranesDetallesDAL.ObtenerAlbaranDetallePorId(id);
                if (albaranDetalle == null)
                {
                    salida = NotFound("No se ha encontrado un albaranDetalle con ese id");
                }
                else
                {
                    salida = Ok(albaranDetalle);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }

            return salida;
        }

        [HttpPost]
        public async Task<IActionResult> CrearAlbaranDetalle([FromBody] clsAlbaranDetalle albaranDetalle)
        {
            IActionResult salida;
            try
            {
                var resultado = await _albaranesDetallesDAL.InsertarAlbaranDetalle(albaranDetalle);
                if (resultado)
                    salida = Ok("AlbaranDetalle creado correctamente");
                else
                    salida = BadRequest("No se pudo crear el albaranDetalle");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarAlbaranDetalle(int id, [FromBody] clsAlbaranDetalle albaranDetalle)
        {
            IActionResult salida;
            try
            {
                if (id != albaranDetalle.IdAlbaranDetalle)
                    salida = BadRequest("El ID de la URL no coincide con el del objeto");

                var albaranDetalleExistente = await _albaranesDetallesDAL.ObtenerAlbaranDetallePorId(id);
                if (albaranDetalleExistente == null)
                    salida = NotFound("AlbaranDetalle no encontrado");

                var resultado = await _albaranesDetallesDAL.ActualizarAlbaranDetalle(albaranDetalle);
                salida = resultado ? Ok("AlbaranDetalle actualizado correctamente") : BadRequest("No se pudo actualizar el albaranDetalle");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarAlbaranDetalle(int id)
        {
            IActionResult salida;
            try
            {
                var resultado = await _albaranesDetallesDAL.EliminarAlbaranDetalle(id);
                salida = resultado ? Ok("AlbaranDetalle eliminado correctamente") : NotFound("No se encontró el albaranDetalle para eliminar");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }
    }
}
