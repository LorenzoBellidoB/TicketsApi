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
                    salida = NotFound("No se han encontrado empresas");
                }
                else
                {
                    salida = Ok(empresas);
                }
            }
            catch(Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }
            return Ok(salida);
        }

        [HttpGet("empresas/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            IActionResult salida;
            try
            {
            var empresa = await _empresasDAL.obtenerEmpresaPorId(id);
                if (empresa == null)
                {
                    salida = NotFound("No se ha encontrado una empresa con ese id");
                }
                else
                {
                    salida = Ok(empresa);
                }
            }
            catch(Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }

            return Ok(salida);
        }

        [HttpPost("empresas")]
        public async Task<IActionResult> CrearEmpresa([FromBody] clsEmpresa empresa)
        {
            try
            {
                var resultado = await _empresasDAL.insertarEmpresa(empresa);
                if (resultado)
                    return Ok("Empresa creada correctamente");
                else
                    return BadRequest("No se pudo crear la empresa");
            }
            catch (Exception e)
            {
                return BadRequest("Error con el servidor: " + e.Message);
            }
        }

        [HttpPut("empresas/{id}")]
        public async Task<IActionResult> ActualizarEmpresa(int id, [FromBody] clsEmpresa empresa)
        {
            try
            {
                if (id != empresa.IdEmpresa)
                    return BadRequest("El ID de la URL no coincide con el del objeto");

                var empresaExistente = await _empresasDAL.obtenerEmpresaPorId(id);
                if (empresaExistente == null)
                    return NotFound("Empresa no encontrada");

                var resultado = await _empresasDAL.actualizarEmpresa(empresa);
                return resultado ? Ok("Empresa actualizada correctamente") : BadRequest("No se pudo actualizar la empresa");
            }
            catch (Exception e)
            {
                return BadRequest("Error con el servidor: " + e.Message);
            }
        }

        [HttpDelete("empresas/{id}")]
        public async Task<IActionResult> EliminarEmpresa(int id)
        {
            try
            {
                var resultado = await _empresasDAL.eliminarEmpresa(id);
                return resultado ? Ok("Empresa eliminada correctamente") : NotFound("No se encontró la empresa para eliminar");
            }
            catch (Exception e)
            {
                return BadRequest("Error con el servidor: " + e.Message);
            }
        }
        }
}
