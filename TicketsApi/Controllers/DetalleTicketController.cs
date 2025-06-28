using DAL;
using ENT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
