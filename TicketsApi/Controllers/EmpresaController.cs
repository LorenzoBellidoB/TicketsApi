using DAL;
using ENT;
using Microsoft.AspNetCore.Mvc;

namespace TicketsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly clsProductosDAL _productosDAL;

        public EmpresaController(clsProductosDAL productosDAL)
        {
            _productosDAL = productosDAL;
        }

        [HttpGet("productos")]
        public async Task<IActionResult> GetProductos()
        {
            var productos = await _productosDAL.obtenerProductos();
            return Ok(productos);
        }

        // Los métodos MVC (Views) los puedes eliminar si solo es API
        // Pero si los dejas para scaffold, mantenlos así:

        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            return Ok($"Detalles del producto {id} (simulado)");
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] object dummy) // Ajusta el modelo real
        {
            try
            {
                return Ok("Producto creado (simulado)");
            }
            catch
            {
                return BadRequest("Error al crear el producto");
            }
        }

        [HttpPut("edit/{id}")]
        public IActionResult Edit(int id, [FromBody] object dummy)
        {
            try
            {
                return Ok($"Producto {id} editado (simulado)");
            }
            catch
            {
                return BadRequest("Error al editar el producto");
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok($"Producto {id} eliminado (simulado)");
            }
            catch
            {
                return BadRequest("Error al eliminar el producto");
            }
        }
    }
}
