using DAL;
using ENT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicketsApi.Controllers
{
    public class ProductoUnidadController : Controller
    {
        private readonly clsProductosUnidadesDAL _productosUnidadesDAL;

        public ProductoUnidadController(clsProductosUnidadesDAL productoUnidadDAL)
        {
            _productosUnidadesDAL = productoUnidadDAL;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductosUnidades()
        {
            IActionResult salida;
            try
            {
                var productosUnidades = await _productosUnidadesDAL.ObtenerProductoUnidades();
                if (productosUnidades.Count == 0)
                {
                    salida = NotFound("No se han encontrado productosUnidades");
                }
                else
                {
                    salida = Ok(productosUnidades);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }
            return salida;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductoUnidad(int id)
        {
            IActionResult salida;
            try
            {
                var productoUnidad = await _productosUnidadesDAL.ObtenerProductoUnidadPorId(id);
                if (productoUnidad == null)
                {
                    salida = NotFound("No se ha encontrado un productoUnidad con ese id");
                }
                else
                {
                    salida = Ok(productoUnidad);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }

            return salida;
        }

        [HttpGet("producto/{id}")]
        public async Task<IActionResult> GetProductoUnidadesPorProductoId(int id)
        {
            IActionResult salida;
            try
            {
                var productosUnidades = await _productosUnidadesDAL.ObtenerProductoUnidadesPorProductoId(id);
                if (productosUnidades == null)
                {
                    salida = NotFound("No se ha encontrado producto unidad asociado a ese producto con ese id");
                }
                else
                {
                    salida = Ok(productosUnidades);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }

            return salida;
        }

        [HttpPost]
        public async Task<IActionResult> CrearProductoUnidad([FromBody] clsProductoUnidad productoUnidad)
        {
            IActionResult salida;
            try
            {
                var resultado = await _productosUnidadesDAL.InsertarProductoUnidad(productoUnidad);
                if (resultado)
                    salida = Ok("ProductoUnidad creado correctamente");
                else
                    salida = BadRequest("No se pudo crear el productoUnidad");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProductoUnidad(int id, [FromBody] clsProductoUnidad productoUnidad)
        {
            IActionResult salida;
            try
            {
                if (id != productoUnidad.IdProductoUnidad)
                    salida = BadRequest("El ID de la URL no coincide con el del objeto");

                var productoUnidadExistente = await _productosUnidadesDAL.ObtenerProductoUnidadPorId(id);
                if (productoUnidadExistente == null)
                    salida = NotFound("ProductoUnidad no encontrado");

                var resultado = await _productosUnidadesDAL.ActualizarProductoUnidad(productoUnidad);
                salida = resultado ? Ok("ProductoUnidad actualizado correctamente") : BadRequest("No se pudo actualizar el productoUnidad");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProductoUnidad(int id)
        {
            IActionResult salida;
            try
            {
                var resultado = await _productosUnidadesDAL.EliminarProductoUnidad(id);
                salida = resultado ? Ok("ProductoUnidad eliminado correctamente") : NotFound("No se encontró el productoUnidad para eliminar");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }
    }
}
