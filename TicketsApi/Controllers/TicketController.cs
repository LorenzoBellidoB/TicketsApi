using DAL;
using ENT;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace TicketsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : Controller
    {
        private readonly clsTicketsDAL _ticketDAL;

        public TicketController(clsTicketsDAL ticketDAL)
        {
            _ticketDAL = ticketDAL;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene un listado con todos los tickets",
            Description = "Este método obtiene todos los tickets y los devuelve como un listado.<br>" +
            "Si no se encuentra ningún ticket devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetTickets()
        {
            IActionResult salida;
            try
            {
                var tickets = await _ticketDAL.ObtenerTickets();
                if (tickets.Count == 0)
                {
                    salida = NotFound("No se han encontrado tickets");
                }
                else
                {
                    salida = Ok(tickets);
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
            Summary = "Obtiene un ticket según su id",
            Description = "Este método obtiene el ticket que coincida con el id proporcionado.<br>" +
            "Si no se encuentra ningún ticket devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetTicket(int id)
        {
            IActionResult salida;
            try
            {
                var ticket = await _ticketDAL.ObtenerTicketPorId(id);
                if (ticket == null)
                {
                    salida = NotFound("No se ha encontrado un ticket con ese id");
                }
                else
                {
                    salida = Ok(ticket);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }

            return salida;
        }
        [HttpGet("{id}/detalles")]
        public async Task<ActionResult<clsTicket>> GetTicketDetalle(int id)
        {
            var ticket = await _ticketDAL.ObtenerTicketCompletoPorId(id);
            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        [HttpGet("empresa/{idEmpresa}")]
        [SwaggerOperation(
            Summary = "Obtiene un ticket según su empresa",
            Description = "Este método obtiene los tickets que coincida con el id de la empresa proporcionado.<br>" +
            "Si no se encuentra ningún ticket devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetTicketsPorEmpresa(int idEmpresa)
        {
            IActionResult salida;
            try
            {
                var tickets = await _ticketDAL.ObtenerTicketsPorIdEmpresa(idEmpresa);
                if (tickets == null)
                {
                    salida = NotFound("No se ha encontrado un ticket con ese id de empresa");
                }
                else
                {
                    salida = Ok(tickets);
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
            Summary = "Crea un nuevo ticket",
            Description = "Este método crea un nuevo ticket con los datos proporcionados.<br>" +
            "Si se crea correctamente devuelve un mensaje de éxito, de lo contrario un mensaje de error."
        )]
        public async Task<IActionResult> CrearTicket([FromBody] clsTicket ticket)
        {
            IActionResult salida;
            try
            {
                var resultado = await _ticketDAL.InsertarTicket(ticket);
                if (resultado)
                    salida = Ok("Ticket creada correctamente");
                else
                    salida = BadRequest("No se pudo crear el ticket");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Actualiza un ticket existente",
            Description = "Este método actualiza un ticket existente con los datos proporcionados.<br>" +
            "Si la actualización es exitosa, devuelve un mensaje de éxito, de lo contrario un mensaje de error."
        )]
        public async Task<IActionResult> ActualizarTicket(int id, [FromBody] clsTicket ticket)
        {
            IActionResult salida;
            try
            {
                if (id != ticket.IdTicket)
                    salida = BadRequest("El ID de la URL no coincide con el del objeto");

                var ticketExistente = await _ticketDAL.ObtenerTicketPorId(id);
                if (ticketExistente == null)
                    salida = NotFound("Ticket no encontrado");

                var resultado = await _ticketDAL.ActualizarTicket(ticket);
                salida = resultado ? Ok("Ticket actualizado correctamente") : BadRequest("No se pudo actualizar el ticket");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Elimina un ticket existente",
            Description = "Este método elimina un ticket existente según su id.<br>" +
            "Si la eliminación es exitosa, devuelve un mensaje de éxito, de lo contrario un mensaje de error."
        )]
        public async Task<IActionResult> EliminarTicket(int id)
        {
            IActionResult salida;
            try
            {
                var resultado = await _ticketDAL.EliminarTicket(id);
                salida = resultado ? Ok("Ticket eliminado correctamente") : NotFound("No se encontró el ticket para eliminar");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }
    }
}
