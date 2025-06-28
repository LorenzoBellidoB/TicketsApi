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
                var productos = await _productoDAL.obtenerProductos();
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
                var producto = await _productoDAL.obtenerProductos(id);
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
        public async Task<IActionResult> CrearEmpresa([FromBody] clsEmpresa empresa)
        {
            IActionResult salida;
            try
            {
                var resultado = await _productoDAL.InsertarEmpresa(empresa);
                if (resultado)
                    salida = Ok("Empresa creada correctamente");
                else
                    salida = BadRequest("No se pudo crear la empresa");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarEmpresa(int id, [FromBody] clsEmpresa empresa)
        {
            IActionResult salida;
            try
            {
                if (id != empresa.IdEmpresa)
                    salida = BadRequest("El ID de la URL no coincide con el del objeto");

                var empresaExistente = await _empresasDAL.ObtenerEmpresaPorId(id);
                if (empresaExistente == null)
                    salida = NotFound("Empresa no encontrada");

                var resultado = await _empresasDAL.ActualizarEmpresa(empresa);
                salida = resultado ? Ok("Empresa actualizada correctamente") : BadRequest("No se pudo actualizar la empresa");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarEmpresa(int id)
        {
            IActionResult salida;
            try
            {
                var resultado = await _empresasDAL.EliminarEmpresa(id);
                salida = resultado ? Ok("Empresa eliminada correctamente") : NotFound("No se encontró la empresa para eliminar");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }
    }
}
