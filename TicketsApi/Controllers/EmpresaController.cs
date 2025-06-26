using DAL;
using ENT;
using Microsoft.AspNetCore.Mvc;

namespace TicketsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly clsEmpresasDAL _empresasDAL;

        public EmpresaController(clsEmpresasDAL empresasDAL)
        {
            _empresasDAL = empresasDAL;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmpresas()
        {
            IActionResult salida;
            try
            {
            var empresas = await _empresasDAL.ObtenerEmpresas();
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
            return salida;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpresa(int id)
        {
            IActionResult salida;
            try
            {
            var empresa = await _empresasDAL.ObtenerEmpresaPorId(id);
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

            return salida;
        }

        [HttpPost]
        public async Task<IActionResult> CrearEmpresa([FromBody] clsEmpresa empresa)
        {
            IActionResult salida;
            try
            {
                var resultado = await _empresasDAL.InsertarEmpresa(empresa);
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
