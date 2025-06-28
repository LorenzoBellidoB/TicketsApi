using DAL;
using ENT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicketsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly clsProductosDAL _productoDAL;

        public ProductoController(clsProductosDAL productoDAL)
        {
            _productoDAL = productoDAL;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            IActionResult salida;
            try
            {
                var productos = await _productoDAL.ObtenerProductos();
                if (productos.Count == 0)
                {
                    salida = NotFound("No se han encontrado productos");
                }
                else
                {
                    salida = Ok(productos);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }
            return salida;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto(int id)
        {
            IActionResult salida;
            try
            {
                var producto = await _productoDAL.ObtenerProductoPorId(id);
                if (producto == null)
                {
                    salida = NotFound("No se ha encontrado un producto con ese id");
                }
                else
                {
                    salida = Ok(producto);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }

            return salida;
        }

        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] clsProducto producto)
        {
            IActionResult salida;
            try
            {
                var resultado = await _productoDAL.InsertarProducto(producto);
                if (resultado)
                    salida = Ok("Producto creada correctamente");
                else
                    salida = BadRequest("No se pudo crear el producto");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] clsProducto producto)
        {
            IActionResult salida;
            try
            {
                if (id != producto.IdProducto)
                    salida = BadRequest("El ID de la URL no coincide con el del objeto");

                var productoExistente = await _productoDAL.ObtenerProductoPorId(id);
                if (productoExistente == null)
                    salida = NotFound("Producto no encontrado");

                var resultado = await _productoDAL.ActualizarProducto(producto);
                salida = resultado ? Ok("Producto actualizado correctamente") : BadRequest("No se pudo actualizar el producto");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            IActionResult salida;
            try
            {
                var resultado = await _productoDAL.EliminarProducto(id);
                salida = resultado ? Ok("Producto eliminado correctamente") : NotFound("No se encontró el producto para eliminar");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }
    }
}
