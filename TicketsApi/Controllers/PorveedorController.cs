using DAL;
using ENT;
using Microsoft.AspNetCore.Mvc;

namespace TicketsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedorController : ControllerBase
    {
        private readonly clsProveedoresDAL _proveedorDAL;

        public ProveedorController(clsProveedoresDAL proveedorDAL)
        {
            _proveedorDAL = proveedorDAL;
        }

        [HttpGet]
        public async Task<IActionResult> GetProveedores()
        {
            IActionResult salida;
            try
            {
                var proveedores = await _proveedorDAL.ObtenerProveedores();
                if (proveedores.Count == 0)
                {
                    salida = NotFound("No se han encontrado proveedores");
                }
                else
                {
                    salida = Ok(proveedores);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }
            return salida;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProveedor(int id)
        {
            IActionResult salida;
            try
            {
                var proveedor = await _proveedorDAL.ObtenerProveedorPorId(id);
                if (proveedor == null)
                {
                    salida = NotFound("No se ha encontrado un proveedor con ese id");
                }
                else
                {
                    salida = Ok(proveedor);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }

            return salida;
        }

        [HttpPost]
        public async Task<IActionResult> CrearProveedor([FromBody] clsProveedor proveedor)
        {
            IActionResult salida;
            try
            {
                var resultado = await _proveedorDAL.InsertarProveedor(proveedor);
                if (resultado)
                    salida = Ok("Proveedor creado correctamente");
                else
                    salida = BadRequest("No se pudo crear el proveedor");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProveedor(int id, [FromBody] clsProveedor proveedor)
        {
            IActionResult salida;
            try
            {
                if (id != proveedor.IdProveedor)
                    salida = BadRequest("El ID de la URL no coincide con el del objeto");

                var proveedorExistente = await _proveedorDAL.ObtenerProveedorPorId(id);
                if (proveedorExistente == null)
                    salida = NotFound("Proveedor no encontrada");

                var resultado = await _proveedorDAL.ActualizarProveedor(proveedor);
                salida = resultado ? Ok("Proveedor actualizado correctamente") : BadRequest("No se pudo actualizar el proveedor");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProveedor(int id)
        {
            IActionResult salida;
            try
            {
                var resultado = await _proveedorDAL.EliminarProveedor(id);
                salida = resultado ? Ok("Proveedor eliminado correctamente") : NotFound("No se encontró el proveedor para eliminar");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }
    }
    
}
