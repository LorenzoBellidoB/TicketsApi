using DAL;
using DTO;
using ENT;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace TicketsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedorController : ControllerBase
    {
        private readonly clsProveedoresDAL _proveedorDAL;

        public ProveedorController(clsProveedoresDAL proveedorDAL)
        {
            _proveedorDAL = proveedorDAL;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene un listado con todos los proveedores",
            Description = "Este método obtiene todos los proveedores y los devuelve como un listado.<br>" +
            "Si no se encuentra ningún proveedor devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetProveedores()
        {
            IActionResult salida;
            try
            {
                var proveedores = await _proveedorDAL.ObtenerProveedores();
                if (proveedores.Count == 0)
                {
                    salida = NotFound("No se han encontrado proveedores");
                }
                else
                {
                    salida = Ok(proveedores);
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
            Summary = "Obtiene un proveedor según su id",
            Description = "Este método obtiene el proveedor que coincida con el id proporcionado.<br>" +
            "Si no se encuentra ningún proveedor devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetProveedor(int id)
        {
            IActionResult salida;
            try
            {
                var proveedor = await _proveedorDAL.ObtenerProveedorPorId(id);
                if (proveedor == null)
                {
                    salida = NotFound("No se ha encontrado un proveedor con ese id");
                }
                else
                {
                    salida = Ok(proveedor);
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
            Summary = "Obtiene un proveedor según su empresa",
            Description = "Este método obtiene los proveedores que coincida con el id de la empresa proporcionado.<br>" +
            "Si no se encuentra ningún proveedor devuelve un mensaje de error."
        )]
        public async Task<IActionResult> GetProveedoresPorEmpresa(int idEmpresa)
        {
            IActionResult salida;
            try
            {
                var proveedores = await _proveedorDAL.ObtenerProveedoresPorIdEmpresa(idEmpresa);
                if (proveedores == null)
                {
                    salida = NotFound("No se ha encontrado un dependiente con ese id de empresa");
                }
                else
                {
                    salida = Ok(proveedores);
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
            Summary = "Crea un nuevo proveedor",
            Description = "Este método crea un nuevo proveedor con los datos proporcionados.<br>" +
            "Si la creación es exitosa, devuelve un mensaje de éxito."
        )]
        public async Task<IActionResult> CrearProveedor([FromBody] ProveedorDTO dto)
        {
            IActionResult salida;
            try
            {
                clsProveedor proveedor = new clsProveedor
                {
                    IdProveedor = dto.IdProveedor,
                    Nombre = dto.Nombre,
                    Correo = dto.Correo,
                    Telefono = dto.Telefono,
                    Cif = dto.Cif,
                    Calle = dto.Calle,
                    Codigo_postal = dto.Codigo_postal,
                    Provincia = dto.Provincia,
                    Localidad = dto.Localidad,
                    IdEmpresa = dto.IdEmpresa
                };
                var resultado = await _proveedorDAL.InsertarProveedor(proveedor);
                if (resultado)
                    salida = Ok("Proveedor creado correctamente");
                else
                    salida = BadRequest("No se pudo crear el proveedor");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Actualiza un proveedor existente",
            Description = "Este método actualiza un proveedor existente con los datos proporcionados.<br>" +
            "Si la actualización es exitosa, devuelve un mensaje de éxito."
        )]
        public async Task<IActionResult> ActualizarProveedor(int id, [FromBody] ProveedorDTO dto)
        {
            IActionResult salida;
            try
            {
                
                if (id != dto.IdProveedor)
                {
                    salida = BadRequest("El ID de la URL no coincide con el del objeto");
                    var proveedorExistente = await _proveedorDAL.ObtenerProveedorPorId(id);
                                    if (proveedorExistente == null)
                    {
                                        salida = NotFound("Proveedor no encontrada");
                    }
                }else
                {
                    clsProveedor proveedor = new clsProveedor
                    {
                        IdProveedor = dto.IdProveedor,
                        Nombre = dto.Nombre,
                        Correo = dto.Correo,
                        Telefono = dto.Telefono,
                        Cif = dto.Cif,
                        Calle = dto.Calle,
                        Codigo_postal = dto.Codigo_postal,
                        Provincia = dto.Provincia,
                        Localidad = dto.Localidad,
                        IdEmpresa = dto.IdEmpresa
                    };
                    var resultado = await _proveedorDAL.ActualizarProveedor(proveedor);
                    salida = resultado ? Ok("Proveedor actualizado correctamente") : BadRequest("No se pudo actualizar el proveedor");
                }
            }
            catch (Exception e)
            {
                var mensaje = e.InnerException?.Message ?? e.Message;
                salida = BadRequest("Error con el servidor: " + mensaje);
            }

            return salida;
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Elimina un proveedor existente",
            Description = "Este método elimina un proveedor existente según su ID.<br>" +
            "Si la eliminación es exitosa, devuelve un mensaje de éxito."
        )]
        public async Task<IActionResult> EliminarProveedor(int id)
        {
            IActionResult salida;
            try
            {
                var resultado = await _proveedorDAL.EliminarProveedor(id);
                salida = resultado ? Ok("Proveedor eliminado correctamente") : NotFound("No se encontró el proveedor para eliminar");
            }
            catch (Exception e)
            {
                salida = BadRequest("Error con el servidor: " + e.Message);
            }
            return salida;
        }
    }
    
}
