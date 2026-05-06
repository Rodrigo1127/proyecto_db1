using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.Interop.API.Data;
using Hospital.Interop.API.Models;

namespace Hospital.Interop.API.Controllers
{
    [ApiController]
    [Route("api/pacientes")]
    [Tags("Gestión de Pacientes")]
    public class PacientesController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public PacientesController(HospitalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var pacientes = await _context.Pacientes.ToListAsync();
                return Ok(pacientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al obtener pacientes",
                    detalle = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var paciente = await _context.Pacientes.FindAsync(id);

                if (paciente == null)
                    return NotFound(new { mensaje = "Paciente no encontrado" });

                return Ok(paciente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al obtener el paciente",
                    detalle = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Paciente paciente)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                paciente.PacienteId = 0;
                paciente.FechaRegistro = DateTime.UtcNow;
                paciente.Activo = true;

                if (paciente.FechaNacimiento.HasValue)
                {
                    paciente.FechaNacimiento = DateTime.SpecifyKind(
                        paciente.FechaNacimiento.Value,
                        DateTimeKind.Utc
                    );
                }

                _context.Pacientes.Add(paciente);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = paciente.PacienteId }, paciente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al guardar el paciente",
                    detalle = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Paciente paciente)
        {
            try
            {
                if (id != paciente.PacienteId)
                    return BadRequest(new { mensaje = "El id de la URL no coincide con el del paciente" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existente = await _context.Pacientes.FindAsync(id);

                if (existente == null)
                    return NotFound(new { mensaje = "Paciente no encontrado" });

                existente.Nombre = paciente.Nombre;
                existente.Documento = paciente.Documento;
                existente.Telefono = paciente.Telefono;
                existente.Direccion = paciente.Direccion;
                existente.Email = paciente.Email;
                existente.Genero = paciente.Genero;
                existente.Activo = paciente.Activo;

                if (paciente.FechaNacimiento.HasValue)
                {
                    existente.FechaNacimiento = DateTime.SpecifyKind(
                        paciente.FechaNacimiento.Value,
                        DateTimeKind.Utc
                    );
                }
                else
                {
                    existente.FechaNacimiento = null;
                }

                await _context.SaveChangesAsync();

                return Ok(existente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al actualizar el paciente",
                    detalle = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var paciente = await _context.Pacientes.FindAsync(id);

                if (paciente == null)
                    return NotFound(new { mensaje = "Paciente no encontrado" });

                _context.Pacientes.Remove(paciente);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al eliminar el paciente",
                    detalle = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }

        [HttpGet("buscar")]
        [ProducesResponseType(typeof(List<Paciente>), 200)]
        public async Task<IActionResult> Buscar([FromQuery] string? nombre = null, [FromQuery] string? documento = null)
        {
            try
            {
                var query = _context.Pacientes.AsQueryable();

                if (!string.IsNullOrWhiteSpace(nombre))
                {
                    query = query.Where(p => p.Nombre.Contains(nombre));
                }

                if (!string.IsNullOrWhiteSpace(documento))
                {
                    query = query.Where(p => p.Documento.Contains(documento));
                }

                var pacientes = await query.ToListAsync();
                return Ok(pacientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al buscar pacientes",
                    detalle = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }
    }
}