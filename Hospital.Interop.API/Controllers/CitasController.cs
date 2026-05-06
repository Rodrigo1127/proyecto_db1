using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.Interop.API.Data;
using Hospital.Interop.API.Models;

namespace Hospital.Interop.API.Controllers
{
    [ApiController]
    [Route("api/citas")]
    [Tags("Citas Médicas")]
    public class CitasController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public CitasController(HospitalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var citas = await _context.Citas
                    .OrderBy(c => c.Fecha)
                    .ToListAsync();

                return Ok(citas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al obtener las citas",
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
                var cita = await _context.Citas.FindAsync(id);

                if (cita == null)
                    return NotFound(new { mensaje = "Cita no encontrada" });

                return Ok(cita);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al obtener la cita",
                    detalle = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }

        [HttpGet("paciente/{pacienteId}")]
        public async Task<IActionResult> GetByPaciente(int id)
        {
            try
            {
                var citas = await _context.Citas
                    .Where(c => c.PacienteId == id)
                    .OrderBy(c => c.Fecha)
                    .ToListAsync();

                return Ok(citas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al obtener citas del paciente",
                    detalle = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar(
            [FromQuery] int? pacienteId = null,
            [FromQuery] string? estado = null,
            [FromQuery] string? departamento = null,
            [FromQuery] DateTime? fechaInicio = null,
            [FromQuery] DateTime? fechaFin = null)
        {
            try
            {
                var query = _context.Citas.AsQueryable();

                if (pacienteId.HasValue)
                    query = query.Where(c => c.PacienteId == pacienteId.Value);

                if (!string.IsNullOrWhiteSpace(estado))
                    query = query.Where(c => c.Estado == estado);

                if (!string.IsNullOrWhiteSpace(departamento))
                    query = query.Where(c => c.Departamento == departamento);

                if (fechaInicio.HasValue)
                {
                    var inicio = DateTime.SpecifyKind(fechaInicio.Value, DateTimeKind.Utc);
                    query = query.Where(c => c.Fecha >= inicio);
                }

                if (fechaFin.HasValue)
                {
                    var fin = DateTime.SpecifyKind(fechaFin.Value, DateTimeKind.Utc);
                    query = query.Where(c => c.Fecha <= fin);
                }

                var citas = await query.OrderBy(c => c.Fecha).ToListAsync();
                return Ok(citas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al buscar citas",
                    detalle = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cita cita)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                cita.Id = 0;

                cita.Fecha = DateTime.SpecifyKind(
                    cita.Fecha,
                    DateTimeKind.Utc
                );

                if (string.IsNullOrWhiteSpace(cita.Estado))
                    cita.Estado = "Pendiente";

                _context.Citas.Add(cita);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = cita.Id }, cita);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al guardar la cita",
                    detalle = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Cita cita)
        {
            try
            {
                if (id != cita.Id)
                    return BadRequest(new { mensaje = "El id de la URL no coincide con el de la cita" });

                var existente = await _context.Citas.FindAsync(id);

                if (existente == null)
                    return NotFound(new { mensaje = "Cita no encontrada" });

                existente.PacienteId = cita.PacienteId;
                existente.Fecha = DateTime.SpecifyKind(cita.Fecha, DateTimeKind.Utc);
                existente.Hora = cita.Hora;
                existente.Departamento = cita.Departamento;
                existente.Estado = cita.Estado;
                existente.Observaciones = cita.Observaciones;

                await _context.SaveChangesAsync();

                return Ok(existente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al actualizar la cita",
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
                var cita = await _context.Citas.FindAsync(id);

                if (cita == null)
                    return NotFound(new { mensaje = "Cita no encontrada" });

                _context.Citas.Remove(cita);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al eliminar la cita",
                    detalle = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }
    }
}