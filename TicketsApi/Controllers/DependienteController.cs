using DAL;
using ENT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicketsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DependienteController : ControllerBase
    {
        private readonly clsDependientesDAL _dependientesDAL;

        public DependienteController(clsDependientesDAL dependienteDAL)
        {
            _dependientesDAL = dependienteDAL;
        }

        [HttpGet]
        public async Task<IActionResult> GetDependientes()
        {
            IActionResult salida;
            try
            {
                var dependientes = await _dependientesDAL.ObtenerDependientes();
                if (dependientes.Count == 0)
                {
                    salida = NotFound("No se han encontrado dependientes");
                }
                else
                {
                    salida = Ok(dependientes);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }
            return salida;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDependiente(int id)
        {
            IActionResult salida;
            try
            {
                var dependiente = await _dependientesDAL.ObtenerDependientePorId(id);
                if (dependiente == null)
                {
                    salida = NotFound("No se ha encontrado un dependiente con ese id");
                }
                else
                {
                    salida = Ok(dependiente);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor " + e.Message);
            }

            return salida;
        }

        [HttpPost]
        public async Task<IActionResult> CrearDependiente([FromBody] clsDependiente dependiente)
        {
            IActionResult salida;
            try
            {
                var resultado = await _dependientesDAL.InsertarDependiente(dependiente);
                if (resultado)
                    salida = Ok("Dependiente creado correctamente");
                else
                    salida = BadRequest("No se pudo crear el dependiente");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarDependiente(int id, [FromBody] clsDependiente dependiente)
        {
            IActionResult salida;
            try
            {
                if (id != dependiente.IdDependiente)
                    salida = BadRequest("El ID de la URL no coincide con el del objeto");

                var dependienteExistente = await _dependientesDAL.ObtenerDependientePorId(id);
                if (dependienteExistente == null)
                    salida = NotFound("Dependiente no encontrado");

                var resultado = await _dependientesDAL.ActualizarDependiente(dependiente);
                salida = resultado ? Ok("Dependiente actualizado correctamente") : BadRequest("No se pudo actualizar el dependiente");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarDependiente(int id)
        {
            IActionResult salida;
            try
            {
                var resultado = await _dependientesDAL.EliminarDependiente(id);
                salida = resultado ? Ok("Dependiente eliminado correctamente") : NotFound("No se encontró el dependiente para eliminar");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }
    }
    
}
