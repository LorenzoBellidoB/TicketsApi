using DAL;
using ENT;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(
            Summary = "Obtiene un listado con todos los productos",
            Description = "Este método obtiene todos los productos y los devuelve como un listado.<br>" +
            "Si no se encuentra ningún producto devuelve un mensaje de error."
        )]
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
        [SwaggerOperation(
            Summary = "Obtiene un producto según su id",
            Description = "Este método obtiene el producto que coincida con el id proporcionado.<br>" +
            "Si no se encuentra ningún producto devuelve un mensaje de error."
        )]
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

        [HttpGet("empresa/{idEmpresa}")]
        [SwaggerOperation(
            Summary = "Obtiene un dependiente según su empresa",
            Description = "Este método obtiene los productos que coincida con el id de la empresa proporcionado.<br>" +
            "Si no se encuentra ningún dependiente devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetProductosPorEmpresa(int idEmpresa)
        {
            IActionResult salida;
            try
            {
                var productos = await _productoDAL.ObtenerProductosPorIdEmpresa(idEmpresa);
                if (productos == null)
                {
                    salida = NotFound("No se ha encontrado un dependiente con ese id de empresa");
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

        [HttpPost]
        [SwaggerOperation(
            Summary = "Crea un nuevo producto",
            Description = "Este método crea un nuevo producto en la base de datos.<br>" +
            "Si la creación es exitosa, devuelve un mensaje de éxito."
        )]
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
        [SwaggerOperation(
            Summary = "Actualiza un producto existente",
            Description = "Este método actualiza un producto existente con los datos proporcionados.<br>" +
            "Si la actualización es exitosa, devuelve un mensaje de éxito."
        )]
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
        [SwaggerOperation(
            Summary = "Elimina un producto existente",
            Description = "Este método elimina un producto existente según su id.<br>" +
            "Si la eliminación es exitosa, devuelve un mensaje de éxito."
        )]
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
