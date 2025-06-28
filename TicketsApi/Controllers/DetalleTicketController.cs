using DAL;
using ENT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace TicketsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleTicketController : Controller
    {
        private readonly clsDetalleTicketsDAL _dTicketDAL;

        public DetalleTicketController(clsDetalleTicketsDAL dTicketDAL)
        {
            _dTicketDAL = dTicketDAL;
        }

        [HttpGet]
        //[SwaggerOperation(
        //    Summary = "Obtiene un listado con todos los detalles de un ticket",
        //    Description = "Este método obtiene todos los detalles de un ticket y los devuelve como un listado.<br>" +
        //    "Si no se encuentra ningún cliente devuelve un mensaje de error."
        //)]
        public async Task<IActionResult> GetDetalles()
        {
            IActionResult salida;
            try
            {
                var dTickets = await _dTicketDAL.ObtenerDetallesTickets();
                if (dTickets.Count == 0)
                {
                    salida = NotFound("No se han encontrado dTickets");
                }
                else
                {
                    salida = Ok(dTickets);
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
        //    Summary = "Obtiene un detalle de un ticket",
        //    Description = "Este método obtiene el detalle de un ticket que coincida con el id proporcionado.<br>" +
        //    "Si no se encuentra ningún dTicket devuelve un mensaje de error."
        //    )]


        public async Task<IActionResult> GetdTicket(int id)
        {
            IActionResult salida;
            try
            {
                var dTicket = await _dTicketDAL.obtenerDetallesPorTicketId(id);
                if (dTicket == null)
                {
                    salida = NotFound("No se ha encontrado un dTicket con ese id");
                }
                else
                {
                    salida = Ok(dTicket);
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
        //    Summary = "Crea un nuevo detalle de ticket",
        //    Description = "Este método crea un nuevo detalle de ticket y lo inserta en la base de datos.<br>" +
        //    "Si se crea correctamente devuelve un mensaje de éxito, si no, devuelve un mensaje de error."
        //)]
        public async Task<IActionResult> CreardTicket([FromBody] clsDetalleTicket dTicket)
        {
            IActionResult salida;
            try
            {
                var resultado = await _dTicketDAL.InsertarDetalleTicket(dTicket);
                if (resultado)
                    salida = Ok("dTicket creada correctamente");
                else
                    salida = BadRequest("No se pudo crear el dTicket");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpPut("{id}")]
        //[SwaggerOperation(
        //    Summary = "Actualiza un detalle de ticket",
        //    Description = "Este método actualiza un detalle de ticket existente con los datos proporcionados en el cuerpo de la solicitud.<br>" +
        //    "Si se actualiza correctamente, devuelve un mensaje de éxito, si no, devuelve un mensaje de error."
        //)]
        public async Task<IActionResult> ActualizardTicket(int id, [FromBody] clsDetalleTicket dTicket)
        {
            IActionResult salida;
            try
            {
                if (id != dTicket.IdTicket)
                    salida = BadRequest("El ID de la URL no coincide con el del objeto");

                var dTicketExistente = await _dTicketDAL.obtenerDetallesPorTicketId(id);
                if (dTicketExistente == null)
                    salida = NotFound("dTicket no encontrado");

                var resultado = await _dTicketDAL.ActualizarDetalleTiquet(dTicket);
                salida = resultado ? Ok("dTicket actualizado correctamente") : BadRequest("No se pudo actualizar el dTicket");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpDelete("{id}")]
        //[SwaggerOperation(
        //    Summary = "Elimina un detalle de ticket",
        //    Description = "Este método elimina un detalle de ticket según el id proporcionado en la URL.<br>" +
        //    "Si se elimina correctamente, devuelve un mensaje de éxito, si no, devuelve un mensaje de error."
        //)]
        public async Task<IActionResult> EliminardTicket(int id)
        {
            IActionResult salida;
            try
            {
                var resultado = await _dTicketDAL.EliminarDetalleTicket(id);
                salida = resultado ? Ok("dTicket eliminado correctamente") : NotFound("No se encontró el dTicket para eliminar");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }
    }
}
