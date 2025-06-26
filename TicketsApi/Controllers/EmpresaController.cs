using DAL;
using ENT;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TicketsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly clsEmpresasDAL _empresasDAL;

        public EmpresaController(clsEmpresasDAL empresasDAL)
        {
            _empresasDAL = empresasDAL;
        }

        [HttpGet("empresas")]
        public async Task<IActionResult> GetEmpresas()
        {
            IActionResult salida;
            try
            {
            var empresas = await _empresasDAL.obtenerEmpresas();
                if(empresas.Count == 0)
                {
                    salida = NotFound();
                }
                else
                {
                    salida = Ok(empresas);
                }
            }
            catch
            {
                salida = BadRequest("Error con el servidor");
            }
            return Ok(salida);
        }

        // Los métodos MVC (Views) los puedes eliminar si solo es API
        // Pero si los dejas para scaffold, mantenlos así:

        [HttpGet("empresas/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            IActionResult salida;
            try
            {
            var empresa = await _empresasDAL.obtenerEmpresaPorId(id);
                if (empresa == null)
                {
                    salida = NotFound();
                }
                else
                {
                    salida = Ok(empresa);
                }
            }
            catch
            {
                salida = BadRequest("Error con el servidor");
            }

            return Ok(salida);
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
