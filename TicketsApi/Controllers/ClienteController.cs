using DAL;
using ENT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicketsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly clsClientesDAL _clientesDAL;

        public ClienteController(clsClientesDAL clienteDAL)
        {
            _clientesDAL = clienteDAL;
        }

        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            IActionResult salida;
            try
            {
                var clientes = await _clientesDAL.ObtenerClientes();
                if (clientes.Count == 0)
                {
                    salida = NotFound("No se han encontrado clientes");
                }
                else
                {
                    salida = Ok(clientes);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }
            return salida;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCliente(int id)
        {
            IActionResult salida;
            try
            {
                var cliente = await _clientesDAL.ObtenerClientePorId(id);
                if (cliente == null)
                {
                    salida = NotFound("No se ha encontrado un cliente con ese id");
                }
                else
                {
                    salida = Ok(cliente);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }

            return salida;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCliente([FromBody] clsCliente cliente)
        {
            IActionResult salida;
            try
            {
                var resultado = await _clientesDAL.InsertarCliente(cliente);
                if (resultado)
                    salida = Ok("Cliente creado correctamente");
                else
                    salida = BadRequest("No se pudo crear el cliente");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCliente(int id, [FromBody] clsCliente cliente)
        {
            IActionResult salida;
            try
            {
                if (id != cliente.IdCliente)
                    salida = BadRequest("El ID de la URL no coincide con el del objeto");

                var clienteExistente = await _clientesDAL.ObtenerClientePorId(id);
                if (clienteExistente == null)
                    salida = NotFound("Cliente no encontrado");

                var resultado = await _clientesDAL.ActualizarCliente(cliente);
                salida = resultado ? Ok("Cliente actualizado correctamente") : BadRequest("No se pudo actualizar el cliente");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            IActionResult salida;
            try
            {
                var resultado = await _clientesDAL.EliminarCliente(id);
                salida = resultado ? Ok("Cliente eliminado correctamente") : NotFound("No se encontró el cliente para eliminar");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }
    }
}
