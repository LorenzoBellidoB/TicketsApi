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
        public IActionResult GetProductos()
        {
            try
            {
                var listadoCompleto = _productosDAL.obtenerProductos();

                if (listadoCompleto == null || listadoCompleto.Count == 0)
                    return NotFound("No se han encontrado productos.");

                return Ok(listadoCompleto);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al conectar a la base de datos:");
                Console.WriteLine(e);
                return StatusCode(500, $"Error interno: {e.Message}");
            }
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
