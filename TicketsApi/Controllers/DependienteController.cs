﻿using DAL;
using DTO;
using ENT;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(
            Summary = "Obtiene un listado con todos los dependientes",
            Description = "Este método obtiene todos los dependientes y los devuelve como un listado.<br>" +
            "Si no se encuentra ningún cliente devuelve un mensaje de error."
        )]
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
        [SwaggerOperation(
            Summary = "Obtiene un dependiente según su id",
            Description = "Este método obtiene el dependiente que coincida con el id proporcionado.<br>" +
            "Si no se encuentra ningún dependiente devuelve un mensaje de error."
        )]
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

        [HttpGet("empresa/{idEmpresa}")]
        [SwaggerOperation(
            Summary = "Obtiene un dependiente según su empresa",
            Description = "Este método obtiene los dependientes que coincida con el id de la empresa proporcionado.<br>" +
            "Si no se encuentra ningún dependiente devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetDependientesPorEmpresa(int idEmpresa)
        {
            IActionResult salida;
            try
            {
                var dependientes = await _dependientesDAL.ObtenerDependientesPorIdEmpresa(idEmpresa);
                if (dependientes == null)
                {
                    salida = NotFound("No se ha encontrado un dependiente con ese id de empresa");
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

        [HttpPost]
        [SwaggerOperation(
            Summary = "Crea un nuevo dependiente",
            Description = "Este método crea un nuevo dependiente con los datos proporcionados.<br>" +
            "Si la creación es exitosa, devuelve un mensaje de éxito."
        )]
        public async Task<IActionResult> CrearDependiente([FromBody] DependienteDTO dto)
        {
            IActionResult salida;
            try
            {
                clsDependiente dependiente = new clsDependiente
                {
                    IdDependiente = dto.IdDependiente,
                    Nombre = dto.Nombre,
                    Correo = dto.Correo,
                    Telefono = dto.Telefono,
                    Dni = dto.Dni,
                    IdEmpresa = dto.IdEmpresa
                };
                var resultado = await _dependientesDAL.InsertarDependiente(dependiente);
                if (resultado > 0)
                    salida = Ok(resultado);
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
        [SwaggerOperation(
            Summary = "Actualiza un dependiente existente",
            Description = "Este método actualiza un dependiente existente con los datos proporcionados en el cuerpo de la solicitud.<br>" +
            "Si la actualización es exitosa, devuelve un mensaje de éxito."
        )]
        public async Task<IActionResult> ActualizarDependiente(int id, [FromBody] DependienteDTO dto)
        {
            IActionResult salida;
            try
            {
                clsDependiente dependiente = new clsDependiente
                {
                    IdDependiente = dto.IdDependiente,
                    Nombre = dto.Nombre,
                    Correo = dto.Correo,
                    Telefono = dto.Telefono,
                    Dni = dto.Dni,
                    IdEmpresa = dto.IdEmpresa
                };
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
        [SwaggerOperation(
            Summary = "Elimina un dependiente según su id",
            Description = "Este método elimina el dependiente que coincida con el id proporcionado.<br>" +
            "Si la eliminación es exitosa, devuelve un mensaje de éxito."
        )]
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
