using DAL;
using DTO;
using ENT;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(
            Summary = "Obtiene un listado con todos los clientes",
            Description = "Este método obtiene todos los clientes y los devuelve como un listado.<br>" +
            "Si no se encuentra ningún cliente devuelve un mensaje de error."
        )]
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
        [SwaggerOperation(
            Summary = "Obtiene un cliente según su id",
            Description = "Este método obtiene el cliente que coincida con el id proporcionado.<br>" +
            "Si no se encuentra ningún cliente devuelve un mensaje de error."
        )]
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

        [HttpGet("empresa/{idEmpresa}")]
        [SwaggerOperation(
            Summary = "Obtiene un cliente según su empresa",
            Description = "Este método obtiene los clientes que coincida con el id de la empresa proporcionado.<br>" +
            "Si no se encuentra ningún dependiente devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetClientesPorEmpresa(int idEmpresa)
        {
            IActionResult salida;
            try
            {
                var clientes = await _clientesDAL.ObtenerClientesPorIdEmpresa(idEmpresa);
                if (clientes == null)
                {
                    salida = NotFound("No se ha encontrado un dependiente con ese id de empresa");
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

        [HttpPost]
        [SwaggerOperation(
            Summary = "Crea un nuevo cliente",
            Description = "Este método permite crear un nuevo cliente. El cliente debe ser proporcionado en el cuerpo de la solicitud."
        )]
        public async Task<IActionResult> CrearCliente([FromBody] ClienteDto dto)
        {
            IActionResult salida;
            try
            {
                clsCliente cliente = new clsCliente
                {
                    IdCliente = dto.IdCliente,
                    Nombre = dto.Nombre,
                    Cif = dto.Cif,
                    Telefono = dto.Telefono,
                    Correo = dto.Correo,
                    Calle = dto.Calle,
                    Codigo_postal = dto.Codigo_postal,
                    Provincia = dto.Provincia,
                    Localidad = dto.Localidad,
                    IdEmpresa = dto.IdEmpresa
                };

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
        [SwaggerOperation(
            Summary = "Actualiza un cliente existente",
            Description = "Este método actualiza un cliente existente. El cliente actualizado debe ser proporcionado en el cuerpo de la solicitud."
        )]
        public async Task<IActionResult> ActualizarCliente(int id, [FromBody] ClienteDto dto)
        {
            IActionResult salida;
            try
            {
                clsCliente cliente = new clsCliente
                {
                    IdCliente = dto.IdCliente,
                    Nombre = dto.Nombre,
                    Cif = dto.Cif,
                    Telefono = dto.Telefono,
                    Correo = dto.Correo,
                    Calle = dto.Calle,
                    Codigo_postal = dto.Codigo_postal,
                    Provincia = dto.Provincia,
                    Localidad = dto.Localidad,
                    IdEmpresa = dto.IdEmpresa
                };
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
        [SwaggerOperation(
            Summary = "Elimina un cliente",
            Description = "Este método elimina un cliente según su id. Si el cliente se elimina correctamente, devuelve un mensaje de éxito, de lo contrario, un mensaje de error."
        )]
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
