using DTO;
using ENT;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DAL;

namespace TicketsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly clsPedidosDAL _pedidoDAL;

        public PedidoController(clsPedidosDAL pedidoDAL)
        {
            _pedidoDAL = pedidoDAL;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene un listado con todos los pedidos",
            Description = "Este método obtiene todos los pedidos y los devuelve como un listado.<br>Si no se encuentra ningún pedido, devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetPedidos()
        {
            try
            {
                var pedidos = await _pedidoDAL.ObtenerPedidos();
                if (pedidos == null || pedidos.Count == 0)
                    return NotFound("No se han encontrado pedidos");

                return Ok(pedidos);
            }
            catch (Exception e)
            {
                return BadRequest("Error con el servidor: " + e.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obtiene un pedido según su id",
            Description = "Este método obtiene el pedido que coincida con el id proporcionado.<br>Si no se encuentra, devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetPedido(int id)
        {
            try
            {
                var pedido = await _pedidoDAL.ObtenerPedidoPorId(id);
                if (pedido == null)
                    return NotFound("No se ha encontrado un pedido con ese id");

                return Ok(pedido);
            }
            catch (Exception e)
            {
                return BadRequest("Error con el servidor: " + e.Message);
            }
        }

        [HttpGet("empresa/{idEmpresa}")]
        [SwaggerOperation(
            Summary = "Obtiene pedidos según el id de empresa",
            Description = "Este método obtiene los pedidos que coincidan con el id de la empresa proporcionado.<br>Si no se encuentra ningún pedido, devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetPedidosPorEmpresa(int idEmpresa)
        {
            try
            {
                var pedidos = await _pedidoDAL.ObtenerPedidosPorIdEmpresa(idEmpresa);
                if (pedidos == null || pedidos.Count == 0)
                    return NotFound("No se han encontrado pedidos para esa empresa");

                return Ok(pedidos);
            }
            catch (Exception e)
            {
                return BadRequest("Error con el servidor: " + e.Message);
            }
        }
        [HttpGet("empresa/{idEmpresa}/entregados")]
        [SwaggerOperation(
            Summary = "Obtiene pedidos según el id de empresa",
            Description = "Este método obtiene los pedidos que coincidan con el id de la empresa proporcionado.<br>Si no se encuentra ningún pedido, devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetPedidosPorEmpresaEntregados(int idEmpresa)
        {
            try
            {
                var pedidos = await _pedidoDAL.ObtenerPedidosPorIdEmpresaEntregados(idEmpresa);
                if (pedidos == null || pedidos.Count == 0)
                    return NotFound("No se han encontrado pedidos para esa empresa");

                return Ok(pedidos);
            }
            catch (Exception e)
            {
                return BadRequest("Error con el servidor: " + e.Message);
            }
        }
        [HttpGet("empresa/{idEmpresa}/noentregados")]
        [SwaggerOperation(
            Summary = "Obtiene pedidos según el id de empresa",
            Description = "Este método obtiene los pedidos que coincidan con el id de la empresa proporcionado.<br>Si no se encuentra ningún pedido, devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetPedidosPorEmpresaNoEntregados(int idEmpresa)
        {
            try
            {
                var pedidos = await _pedidoDAL.ObtenerPedidosPorIdEmpresaNoEntregados(idEmpresa);
                if (pedidos == null || pedidos.Count == 0)
                    return NotFound("No se han encontrado pedidos para esa empresa");

                return Ok(pedidos);
            }
            catch (Exception e)
            {
                return BadRequest("Error con el servidor: " + e.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Crea un nuevo pedido",
            Description = "Este método crea un nuevo pedido con los datos proporcionados en el cuerpo de la solicitud.<br>Si se crea correctamente, devuelve el ID del nuevo pedido."
        )]
        public async Task<IActionResult> CrearPedido([FromBody] PedidoDTO dto)
        {
            var pedido = new clsPedido
            {
                FechaEntrega = dto.FechaEntrega,
                Descripcion = dto.Descripcion,
                FechaCreado = dto.FechaCreado,
                Entregado = dto.Entregado,
                IdCliente = dto.IdCliente,
                IdEmpresa = dto.IdEmpresa,
                IdDependiente = dto.IdDependiente
            };

            var id = await _pedidoDAL.InsertarPedido(pedido);
            if (id <= 0)
                return BadRequest("No se pudo crear el pedido");

            return Ok(id);
        }

        [HttpPatch("{id}/entregar")]
        [SwaggerOperation(
            Summary = "Marca un pedido como entregado",
            Description = "Este método marca como entregado el pedido especificado por su id."
        )]
        public async Task<IActionResult> EntregarPedido(int id)
        {
            var result = await _pedidoDAL.EntregarAsync(id);
            if (!result)
                return BadRequest("No se pudo entregar el pedido");

            return Ok("Pedido entregado correctamente");
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Actualiza un pedido",
            Description = "Este método actualiza un pedido con los datos proporcionados.<br>Debe coincidir el ID del objeto con el de la URL."
        )]
        public async Task<IActionResult> ActualizarPedido(int id, [FromBody] PedidoDTO dto)
        {
            if (id != dto.IdPedido)
                return BadRequest("El ID de la URL no coincide con el del objeto");

            var existente = await _pedidoDAL.ObtenerPedidoPorId(id);
            if (existente == null)
                return NotFound("Pedido no encontrado");

            // Actualizar campos
            existente.FechaEntrega = dto.FechaEntrega;
            existente.Descripcion = dto.Descripcion;
            existente.FechaCreado = dto.FechaCreado;
            existente.Entregado = dto.Entregado;
            existente.IdCliente = dto.IdCliente;
            existente.IdEmpresa = dto.IdEmpresa;
            existente.IdDependiente = dto.IdDependiente;

            var result = await _pedidoDAL.ActualizarPedido(existente);
            if (!result)
                return BadRequest("No se pudo actualizar el pedido");

            return Ok("Pedido actualizado correctamente");
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Elimina un pedido",
            Description = "Este método elimina un pedido según su id.<br>Si se elimina correctamente, devuelve un mensaje de éxito."
        )]
        public async Task<IActionResult> EliminarPedido(int id)
        {
            try
            {
                var result = await _pedidoDAL.EliminarPedido(id);
                if (!result)
                    return NotFound("No se encontró el pedido para eliminar");

                return Ok("Pedido eliminado correctamente");
            }
            catch (Exception e)
            {
                return BadRequest("Error con el servidor: " + e.Message);
            }
        }
    }
}
