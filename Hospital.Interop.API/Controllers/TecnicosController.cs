using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.Interop.API.Data;
using Hospital.Interop.API.Models;

namespace Hospital.Interop.API.Controllers
{
    [ApiController]
    [Route("api/tecnicos")]
    [Tags("Laboratorio Clínico")]
    public class TecnicosController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public TecnicosController(HospitalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Tecnico>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var tecnicos = await _context.Tecnicos
                .Include(t => t.Departamento)
                .Where(t => t.Activo)
                .ToListAsync();

            return Ok(tecnicos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Tecnico), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var tecnico = await _context.Tecnicos
                .Include(t => t.Departamento)
                .FirstOrDefaultAsync(t => t.TecnicoId == id);

            if (tecnico == null)
                return NotFound(new { mensaje = "Técnico no encontrado" });

            return Ok(tecnico);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Tecnico), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] Tecnico tecnico)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Tecnicos.Add(tecnico);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = tecnico.TecnicoId }, tecnico);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(int id, [FromBody] Tecnico tecnico)
        {
            if (id != tecnico.TecnicoId)
                return BadRequest(new { mensaje = "ID no coincide" });

            var existe = await _context.Tecnicos.AnyAsync(t => t.TecnicoId == id);
            if (!existe)
                return NotFound(new { mensaje = "Técnico no encontrado" });

            _context.Entry(tecnico).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(tecnico);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var tecnico = await _context.Tecnicos.FindAsync(id);

            if (tecnico == null)
                return NotFound(new { mensaje = "Técnico no encontrado" });

            tecnico.Activo = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
