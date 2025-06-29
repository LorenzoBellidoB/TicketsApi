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
        //[SwaggerOperation(
        //    Summary = "Obtiene un listado con todos los detalles del albaran",
        //    Description = "Este método obtiene todos los detalles del albaran y los devuelve como un listado.<br>" +
        //    "Si no se encuentra ningún albaran devuelve un mensaje de error."
        //)]
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
        //[SwaggerOperation(
        //    Summary = "Obtiene un detalle del albaran según su id",
        //    Description = "Este método obtiene el detalle del albaran que coincida con el id proporcionado.<br>" +
        //    "Si no se encuentra ningún albaran devuelve un mensaje de error."
        //)]
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
        //[SwaggerOperation(
        //    Summary = "Crea un nuevo detalle de albaran",
        //    Description = "Este método crea un nuevo detalle de albaran a partir de los datos proporcionados en el cuerpo de la solicitud.<br>" +
        //    "Si se crea correctamente, devuelve un mensaje de éxito."
        //)]
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
        //[SwaggerOperation(
        //    Summary = "Actualiza un detalle de albaran",
        //    Description = "Este método actualiza un detalle de albaran según el id proporcionado en la URL y los datos del cuerpo de la solicitud.<br>" +
        //    "Si se actualiza correctamente, devuelve un mensaje de éxito, de lo contrario, un mensaje de error."
        //)]
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
        //[SwaggerOperation(
        //    Summary = "Elimina un detalle de albaran",
        //    Description = "Este método elimina un detalle de albaran según el id proporcionado en la URL.<br>" +
        //    "Si se elimina correctamente, devuelve un mensaje de éxito, de lo contrario, un mensaje de error."
        //)]
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
