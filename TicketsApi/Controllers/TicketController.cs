using DAL;
using ENT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicketsApi.Controllers
{
    public class TicketController : Controller
    {
        private readonly clsTicketsDAL _ticketDAL;

        public TicketController(clsTicketsDAL ticketDAL)
        {
            _ticketDAL = ticketDAL;
        }

        [HttpGet]
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

        [HttpPost]
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
